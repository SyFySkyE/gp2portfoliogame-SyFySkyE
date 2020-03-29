using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThree : MonoBehaviour
{

    public RectTransform gameBoard;

    [Header("Prefabs")]
    public GameObject nodePiece;

    public ArrayLayout1 boardLayout;
    public Sprite[] Pieces;
    private int boardWidth = 8;
    private int boardHeight = 8;
    private Node[,] board;

    System.Random random;

    private void StartGame()
    {        
        string seed = GetRandomSeed();
        random = new System.Random(seed.GetHashCode());
        InitializeBoard();
        VerifyBoard();
        InstantiateBoard();
    }

    private void InitializeBoard()
    {
        board = new Node[boardWidth, boardHeight];
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x <boardWidth; x++)
            {
                board[x, y] = new Node((boardLayout.rows[y].row[x]) ? 0 : FillPiece(), new Point(x, y));
            }
        }
    }

    private void VerifyBoard()
    {
        List<int> remove;
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                Point p = new Point(x, y);
                int val = GetValueAtPoint(p);
                if (val == 0) continue;
                remove = new List<int>();
                while (IsConnected(p, true).Count > 0)
                {
                    val = GetValueAtPoint(p);
                    if (!remove.Contains(val))
                    {
                        remove.Add(val);                        
                    }
                    SetValueAtPoint(p, newValue(ref remove));
                }
            }
        }
    }

    private void InstantiateBoard()
    {
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                int val = board[x, y].value;
                if (val == 0) continue;
                GameObject p = Instantiate(nodePiece, gameBoard);
                RectTransform rect = p.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(50 + (100 * x), -50 - (0 * y));
            }
        }
    }

    private int newValue(ref List<int> remove)
    {
        List<int> available = new List<int>();
        for (int i = 0; i < Pieces.Length; i++)
        {
            available.Add(i + 1);
        }
        foreach (int i in remove)
        {
            available.Remove(i);
        }

        if (available.Count == 0) return 0;
        return available[random.Next(0, available.Count)];
    }

    private void SetValueAtPoint(Point p, int v)
    {
        board[p.x, p.y].value = v;
    }

    List<Point> IsConnected(Point p, bool main)
    {
        List<Point> connected = new List<Point>();
        int val = GetValueAtPoint(p);
        Point[] directions =
        {
            Point.Up,
            Point.Right,
            Point.Down,
            Point.Left
        };

        foreach (Point dir in directions) // Checking if there is 2 or more same pieces in the direction
        {
            List<Point> line = new List<Point>();
            int same = 0;
            
            for (int i = 1; i < 3; i++)
            {
                Point check = Point.Add(p, Point.Mult(dir, i));
                if (GetValueAtPoint(check) == val)
                {
                    line.Add(check);
                    same++;
                }
            }

            if (same > 1) // If there are more than 1 shape in the direction then we know it is a match
            { 
                AddPoints(ref connected, line); // Add these points to the overarching connected list
            }
        }

        for (int i = 0; i < 2; i++) // Checking if we are in the middle of two of the same shapes
        {
            List<Point> line = new List<Point>();
            int same = 0;
            Point[] check = { Point.Add(p, directions[i]), Point.Add(p, directions[i + 2])};
            foreach (Point next in check)
            {
                if (GetValueAtPoint(next) == val)
                {
                    line.Add(next);
                    same++;
                }
            }

            if (same > 1)
            {
                AddPoints(ref connected, line);
            }
        }

        for (int i = 0; i < 4; i++) // Check for a 2x2
        {
            List<Point> square = new List<Point>();
            int same = 0;
            int next = 1 + 1;
            if (next >= 4) next -= 4;

            Point[] check = { Point.Add(p, directions[i]), Point.Add(p, directions[next]), Point.Add(p, Point.Add(directions[i], directions[next])) };
            foreach (Point pnt in check)
            {
                if (GetValueAtPoint(pnt) == val)
                {
                    square.Add(pnt);
                    same++;
                }
            }

            if (same > 2)
            {
                AddPoints(ref connected, square);
            }
        }

        if (main) // Checks for other mtaches along the current match // 
        {
            for (int i = 0; i < connected.Count; i++)
            {
                AddPoints(ref connected, IsConnected(connected[i], false));
            }
        }

        if (connected.Count > 0)
        {
            connected.Add(p);            
        }

        return connected;
    }

    private void AddPoints(ref List<Point> points, List<Point> add)
    {
        foreach (Point p in add)
        {
            bool doAdd = true;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(p))
                {
                    doAdd = false;
                    break;
                }
            }

            if (doAdd)
            {
                points.Add(p);
            }
        }
    }

    private int GetValueAtPoint(Point p)
    {
        if (p.x < 0 || p.x >= boardWidth || p.y < 0 || p.y >= boardHeight) return 0;
        return board[p.x, p.y].value;
    }

    private int FillPiece()
    {
        int val = 1;
        val = random.Next(0, 100) / (100 / Pieces.Length) + 1;
        return val;
    }

    private string GetRandomSeed()
    {
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRSTUVWYZabsdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()";
        for (int i = 0; i < 20; i++)
        {
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }
        return seed;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Node
{
    public int value; // 0 = Blank, 1 = Frost, 2 = Purple, 3 = Red, 4 = Sand, 5 = Water, 6 = Sun
    public Point index;

    public Node(int v, Point i)
    {
        value = v;
        index = i;
    }
}

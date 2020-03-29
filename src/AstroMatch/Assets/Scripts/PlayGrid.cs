using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour
{
    [Header("Grid Parameters")]
    [SerializeField] private int numberOfColumns = 8;
    [SerializeField] private int numberOfRows = 8;

    [SerializeField] private Piece[] playablePieces;

    private UnityEngine.UI.Image playField; // TODO Should this be exposed? Also this breaks when Rows and/or columns get changed!
    private List<IMatachable> inGamePieces = new List<IMatachable>();
    private List<Cell> gridCells = new List<Cell>();

    // Start is called before the first frame update
    void Start()
    {
        playField = GetComponent<UnityEngine.UI.Image>();
        InitializeGrid();        
    }

    private void InitializeGrid() // TODO This needs to be cleaned up!
    {
        float startingX = -((playField.rectTransform.sizeDelta.x / 2) - (playablePieces[0].PieceRectTransform.rect.width / 2)); // TODO Since the size is to never change, should just cache a playablePieces size so there isn't so many playablePieces[0] in here and below
        float startingY = ((playField.rectTransform.sizeDelta.y / 2) - (playablePieces[0].PieceRectTransform.rect.height / 2)); // TODO StartingX and y should be renamed
        Vector2 startingPos = new Vector2(startingX, startingY);
        
        for (int i = 0; i < numberOfColumns; i++)
        {
            for (int j = 0; j < numberOfRows; j++)
            {
                IMatachable newPiece = Instantiate(playablePieces[Random.Range(0, playablePieces.Length)], this.transform, false); 
                newPiece.PieceRectTransform.localPosition = startingPos;
                inGamePieces.Add(newPiece);
                Cell newCell = new Cell(i, j, newPiece);
                gridCells.Add(newCell);
                startingPos.x += playablePieces[0].PieceRectTransform.rect.width;
            }
            startingPos.y -= playablePieces[0].PieceRectTransform.rect.height;
            startingPos.x = -((playField.rectTransform.sizeDelta.x / 2) - (playablePieces[0].PieceRectTransform.rect.width / 2));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gridCells[0].SwapCells(gridCells[1]);
        }
    }
}

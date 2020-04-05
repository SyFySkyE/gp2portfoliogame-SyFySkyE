using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour // TODO Not happy with this class as it's in charge of setting up the playGrid and Matching logic. Can we make Matching logic another ca
{
    [Header("Grid Parameters")]
    [SerializeField] private int numberOfColumns = 8;
    [SerializeField] private int numberOfRows = 8;

    [Header("Playable Piece Prefab")]
    [SerializeField] private NormalPiece playablePiece;

    [Header("Null Piece to fill Outer Cells")]
    [SerializeField] private NullPiece nullPiece; // TODO FInd this via resources. We don't want a certain designer fucking this up somehow (me)

    [Header("An empty cell prefab")]
    [SerializeField] private Cell playableCell;
    
    private Cell[,] cellArray;

    private UnityEngine.UI.Image playField; // TODO Should this be exposed? Also this visually breaks when Rows and/or columns get changed!    

    // Start is called before the first frame update
    void Start()
    {
        cellArray = new Cell[numberOfColumns + 2, numberOfRows + 2];
        playField = GetComponent<UnityEngine.UI.Image>();
        InitializeGrid();        
    }

    private void InitializeGrid() // TODO This needs to be cleaned up!
    {
        float pieceSizeWidth = playablePiece.PieceRectTransform.rect.width;
        float pieceSizeHeight = playablePiece.PieceRectTransform.rect.height;

        float xPiecePlacement = -((playField.rectTransform.sizeDelta.x / 2) - (pieceSizeWidth / 2)); 
        float yPiecePlacement = ((playField.rectTransform.sizeDelta.y / 2) - (pieceSizeHeight / 2)); 
        Vector2 piecePlacement = new Vector2(xPiecePlacement, yPiecePlacement); // Where the pieces should start spawning

        InitializeNullCells();

        for (int currentColumn = 1; currentColumn <= numberOfColumns; currentColumn++)
        {
            for (int currentRow = 1; currentRow <= numberOfRows; currentRow++)
            {
                cellArray[currentRow, currentColumn] = Instantiate(playableCell, this.transform, false);
                cellArray[currentRow, currentColumn].CellLocation = new Vector2(currentRow, currentColumn);               
                cellArray[currentRow, currentColumn].PieceInCell = Instantiate(playablePiece, cellArray[currentRow, currentColumn].transform, false); // TODO This should be done by the cell's START method, not here!
                cellArray[currentRow, currentColumn].PieceInCell.SetupPiece();
                bool doesSpawnMatch = Match.CheckForInitialMatch(cellArray[currentRow, currentColumn], cellArray, Directions.RecursiveDirections); // Ensures that the initial piece spawns does not come with a match three (or more) already in place
                while (doesSpawnMatch) 
                {
                    cellArray[currentRow, currentColumn].PieceInCell.SetupPiece(); // Re randomizes piece type
                    doesSpawnMatch = Match.CheckForInitialMatch(cellArray[currentRow, currentColumn], cellArray, Directions.RecursiveDirections); 
                }                
                cellArray[currentRow, currentColumn].RectTransform.localPosition = piecePlacement;             
                piecePlacement.x += pieceSizeWidth;
            }
            piecePlacement.y -= pieceSizeHeight;
            piecePlacement.x = -((playField.rectTransform.sizeDelta.x / 2) - (pieceSizeWidth / 2));
        }        
    }

    private void InitializeNullCells()
    {
        for (int currentColumn = 0; currentColumn == 0; currentColumn++)
        {
            for (int currentRow = 0; currentRow <= numberOfRows + 1; currentRow++)
            {
                cellArray[currentRow, currentColumn] = Instantiate(playableCell, this.transform, false);
                cellArray[currentRow, currentColumn].CellLocation = new Vector2(currentRow, currentColumn);
                cellArray[currentRow, currentColumn].PieceInCell = Instantiate(nullPiece, cellArray[currentRow, currentColumn].transform, false);
            }
        }

        for (int currentColumn = numberOfColumns + 1; currentColumn == numberOfColumns + 1; currentColumn--)
        {
            for (int currentRow = 0; currentRow <= numberOfRows + 1; currentRow++)
            {
                cellArray[currentRow, currentColumn] = Instantiate(playableCell, this.transform, false);
                cellArray[currentRow, currentColumn].CellLocation = new Vector2(currentRow, currentColumn);
                cellArray[currentRow, currentColumn].PieceInCell = Instantiate(nullPiece, cellArray[currentRow, currentColumn].transform, false);
            }
        }

        for (int currentRow = 0; currentRow == 0; currentRow++)
        {
            for (int currentColumn = 0; currentColumn <= numberOfColumns + 1; currentColumn++)
            {
                cellArray[currentRow, currentColumn] = Instantiate(playableCell, this.transform, false);
                cellArray[currentRow, currentColumn].CellLocation = new Vector2(currentRow, currentColumn);
                cellArray[currentRow, currentColumn].PieceInCell = Instantiate(nullPiece, cellArray[currentRow, currentColumn].transform, false);
            }
        }

        for (int currentRow = numberOfRows + 1; currentRow == numberOfRows + 1; currentRow++)
        {
            for (int currentColumn = 0; currentColumn <= numberOfColumns + 1; currentColumn++)
            {
                cellArray[currentRow, currentColumn] = Instantiate(playableCell, this.transform, false);
                cellArray[currentRow, currentColumn].CellLocation = new Vector2(currentRow, currentColumn);
                cellArray[currentRow, currentColumn].PieceInCell = Instantiate(nullPiece, cellArray[currentRow, currentColumn].transform, false);
            }
        }
    }

    public void SwapPieces(Cell cellOne, Cell cellTwo, bool isSwapBack) 
    {
        Transform cellOneCurrentTransform = cellOne.transform; // Cache transform reference as it will change
        cellOne.PieceInCell.transform.SetParent(cellTwo.transform, false); // Set new parents
        cellTwo.PieceInCell.transform.SetParent(cellOneCurrentTransform, false);        
        cellOne.PieceInCell = cellTwo.PieceInCell;
        cellTwo.PieceInCell = cellOne.InitialPiece;
        if (!isSwapBack)
        {
            bool doesCellOneMatch = Match.CheckForInitialMatch(cellOne, cellArray, Directions.AllDirections);
            bool doesCellTwoMatch = Match.CheckForInitialMatch(cellTwo, cellArray, Directions.AllDirections);
            if (!doesCellOneMatch && !doesCellTwoMatch) 
            {
                SwapPieces(cellTwo, cellOne, true); // No Match, swap the cell piece back to whence it came
                return;
            }
            if (doesCellOneMatch)
            {
                foreach (Cell currentCell in Match.GetConnectedCells(cellOne, cellArray, Directions.AllDirections))
                {
                    currentCell.PieceInCell.Match(); // Send piece back to pool
                }
            }
            if (doesCellTwoMatch)
            {                
                foreach (Cell currentCell in Match.GetConnectedCells(cellTwo, cellArray, Directions.AllDirections))
                {                    
                    currentCell.PieceInCell.Match(); // Send piece back to pool
                }
            }
        }
    }
}
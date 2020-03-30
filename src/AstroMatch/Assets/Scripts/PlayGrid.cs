using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGrid : MonoBehaviour
{
    [Header("Grid Parameters")]
    [SerializeField] private int numberOfColumns = 8;
    [SerializeField] private int numberOfRows = 8;

    [SerializeField] private Piece[] playablePieces;

    private UnityEngine.UI.Image playField; // TODO Should this be exposed? Also this visually breaks when Rows and/or columns get changed!
    private List<IMatachable> inGamePieces = new List<IMatachable>(); // TODO Do we need this?

    // Start is called before the first frame update
    void Start()
    {
        playField = GetComponent<UnityEngine.UI.Image>();
        InitializeGrid();        
    }

    private void InitializeGrid() // TODO This needs to be cleaned up!
    {
        float pieceSizeWidth = playablePieces[0].PieceRectTransform.rect.width;
        float pieceSizeHeight = playablePieces[0].PieceRectTransform.rect.height;

        float xPiecePlacement = -((playField.rectTransform.sizeDelta.x / 2) - (pieceSizeWidth / 2)); 
        float yPiecePlacement = ((playField.rectTransform.sizeDelta.y / 2) - (pieceSizeHeight / 2)); 
        Vector2 piecePlacement = new Vector2(xPiecePlacement, yPiecePlacement); // Where the pieces should start spawning

        for (int currentColumn = 0; currentColumn < numberOfColumns; currentColumn++)
        {
            for (int currentRow = 0; currentRow < numberOfRows; currentRow++)
            {
                IMatachable newPiece = Instantiate(playablePieces[Random.Range(0, playablePieces.Length)], this.transform, false);
                newPiece.PieceRectTransform.localPosition = piecePlacement;
                inGamePieces.Add(newPiece);
                newPiece.CellLocation = new Vector2(currentRow + 1, currentColumn + 1);
                piecePlacement.x += pieceSizeWidth;
            }
            piecePlacement.y -= pieceSizeHeight;
            piecePlacement.x = -((playField.rectTransform.sizeDelta.x / 2) - (pieceSizeWidth / 2));
        }

        CheckForMatch();
    }

    private void CheckForMatch()
    {
        foreach (IMatachable piece in inGamePieces)
        {
            PieceType currentPieceType = piece.PieceCurrentType;
            foreach (Vector2 dir in Directions.directions)
            {
                piece.CellLocation
            }
        }
    }

    private Vector2 GetGridPoint()
    {

    }

    public void SwapPieces(Piece pieceOne, Piece pieceTwo) 
    {
        Vector3 pieceOneTransform = pieceOne.transform.position;
        Vector3 pieceTwoTransform = pieceTwo.transform.position;
        pieceOne.transform.position = pieceTwoTransform;
        pieceTwo.transform.position = pieceOneTransform;
        pieceOne.CellLocation = pieceTwo.CellLocation;
        pieceTwo.CellLocation = pieceOne.InitialCellLocation;
    }
}

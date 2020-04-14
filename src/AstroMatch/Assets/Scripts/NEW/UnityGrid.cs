using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityGrid : MonoBehaviour
{
    [Header("Grid Parameters")]
    [SerializeField] private int numberOfColumns = 8;
    [SerializeField] private int numberOfRows = 8;

    [Header("Unity Piece Prefab")]
    [SerializeField] private UnityEngine.UI.Image unityPiece;

    [Header("TODO Load this from resources")]
    [SerializeField] private Sprite waterImage;
    [SerializeField] private Sprite iceImage;
    [SerializeField] private Sprite redImage;
    [SerializeField] private Sprite purpleImage;
    [SerializeField] private Sprite sandImage;
    [SerializeField] private Sprite emptyImage;

    public SinglePiece[,] PieceArray { get; private set; } // Conceptual Array of pieces
    private List<Image> unityPieces; // Visual GO array
    private UnityEngine.UI.Image playField; // TODO Should this be exposed? Also this visually breaks when Rows and/or columns get changed!    

    // Start is called before the first frame update
    void Start()
    {
        PieceArray = new SinglePiece[numberOfColumns + 2, numberOfRows + 2];
        playField = GetComponent<UnityEngine.UI.Image>();
        InitializeGrid();
    }

    private void InitializeGrid()
    {        
        InitializeNullCells();
        InitializeNormalCells();
        DrawCells();
    }    

    private void InitializeNullCells()
    {
        for (int currentColumn = 0; currentColumn == 0; currentColumn++)
        {
            for (int currentRow = 0; currentRow <= numberOfRows + 1; currentRow++)
            {
                PieceArray[currentRow, currentColumn] = new SinglePiece(currentRow, currentColumn, true); // Makes a new piece, makes it of enum type null
            }
        }

        for (int currentColumn = numberOfColumns + 1; currentColumn == numberOfColumns + 1; currentColumn--)
        {
            for (int currentRow = 0; currentRow <= numberOfRows + 1; currentRow++)
            {
                PieceArray[currentRow, currentColumn] = new SinglePiece(currentRow, currentColumn, true);
            }
        }

        for (int currentRow = 0; currentRow == 0; currentRow++)
        {
            for (int currentColumn = 0; currentColumn <= numberOfColumns + 1; currentColumn++)
            {
                PieceArray[currentRow, currentColumn] = new SinglePiece(currentRow, currentColumn, true);
            }
        }

        for (int currentRow = numberOfRows + 1; currentRow == numberOfRows + 1; currentRow++)
        {
            for (int currentColumn = 0; currentColumn <= numberOfColumns + 1; currentColumn++)
            {
                PieceArray[currentRow, currentColumn] = new SinglePiece(currentRow, currentColumn, true);
            }
        }
    }

    private void InitializeNormalCells()
    {
        //for (int currentColumn = 1; currentColumn <= numberOfColumns; currentColumn++)
        //{
        //    for (int currentRow = 1; currentRow <= numberOfRows; currentRow++)
        //    {
        //        PieceArray[currentRow, currentColumn] = new SinglePiece(currentRow, currentColumn, false);
        //        bool doesSpawnMatch = Matching.DoesMatch(PieceArray[currentRow, currentColumn], PieceArray, Directions.RecursiveDirections);
        //        while (doesSpawnMatch)
        //        {
        //            PieceArray[currentRow, currentColumn].RandomizeType();
        //            doesSpawnMatch = Matching.DoesMatch(PieceArray[currentRow, currentColumn], PieceArray, Directions.RecursiveDirections);
        //        }
        //    }
        //}

        for (int currentRow = 1; currentRow <= numberOfRows; currentRow++)
        {
            for (int currentColumn = 1; currentColumn <= numberOfColumns; currentColumn++)
            {
                PieceArray[currentColumn, currentRow] = new SinglePiece(currentColumn, currentRow, false);
                if (PieceArray[currentColumn, currentRow].PieceType != SinglePieceType.None)
                {
                    bool doesSpawnMatch = Matching.CheckInitialMatch(PieceArray[currentColumn, currentRow], PieceArray, Directions.RecursiveDirections);
                    while (doesSpawnMatch)
                    {
                        PieceArray[currentColumn, currentRow].RandomizeType();
                        doesSpawnMatch = Matching.CheckInitialMatch(PieceArray[currentColumn, currentRow], PieceArray, Directions.RecursiveDirections);
                    }
                }
            }
        }
    }

    private void DrawCells()
    {
        float pieceSizeWidth = unityPiece.rectTransform.rect.width; 
        float pieceSizeHeight = unityPiece.rectTransform.rect.width;

        float xPiecePlacement = -(playField.rectTransform.rect.width / 2) - (pieceSizeWidth / 2);
        float yPiecePlacement = (playField.rectTransform.rect.height / 2) + (pieceSizeHeight / 2);
        Vector2 piecePlacement = new Vector2(xPiecePlacement, yPiecePlacement); // Where the pieces should start spawning

        foreach (SinglePiece currentPiece in PieceArray)
        {
            Image newPiece = Instantiate(unityPiece, this.transform, false);
            newPiece.GetComponent<UnityPiece>().InitializeLocation(currentPiece.Location);
            newPiece.sprite = GetSprite(currentPiece);
            newPiece.rectTransform.localPosition = piecePlacement;
            piecePlacement.x += pieceSizeWidth;
            if (piecePlacement.x > playField.rectTransform.rect.width / 2 + (pieceSizeWidth / 2))
            {
                piecePlacement.y -= pieceSizeHeight;
                piecePlacement.x = -(playField.rectTransform.rect.width / 2) - (pieceSizeWidth / 2);
            }
        }
    }

    private Sprite GetSprite(SinglePiece piece)
    {
        switch (piece.PieceType)
        {
            case SinglePieceType.Ice:
                return iceImage;
            case SinglePieceType.Purple:
                return purpleImage;
            case SinglePieceType.Red:
                return redImage;
            case SinglePieceType.Sand:
                return sandImage;
            case SinglePieceType.Water:
                return waterImage;
            default:
                return emptyImage;
        }
    }

    public void SwapPieces(SinglePiece pieceOne, SinglePiece pieceTwo, bool isSwapBack)
    {
        Vector2 pieceOneInitialLoc = pieceOne.Location;
        pieceOne.Location = pieceTwo.Location;
        pieceTwo.Location = pieceOneInitialLoc;
    }
}

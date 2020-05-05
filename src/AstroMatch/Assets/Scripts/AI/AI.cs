using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class AI
{
    private Grid computerGrid;
    private SinglePiece pieceSelected;
    Vector2 matchDir;

    public AI(Grid enemyGrid)
    {
        computerGrid = new Grid(enemyGrid.numberOfColumns, enemyGrid.numberOfRows);
        this.computerGrid = enemyGrid;
        matchDir = Vector2.zero;
    }

    public SinglePiece AICommand()
    {
        if (this.matchDir == Vector2.zero) // If we haven't found a match yet
        {
            pieceSelected = FindInitialPieceSelection(out matchDir); // Let's find the first piece and where to swap
        }
        else // And swap it
        {
            pieceSelected = computerGrid.PieceArray[(int)pieceSelected.Location.x + (int)matchDir.x, (int)pieceSelected.Location.y + (int)matchDir.y];
            this.matchDir = Vector2.zero;
        }
        return pieceSelected;
    }

    private SinglePiece FindInitialPieceSelection(out Vector2 matchingDir)
    {
        while (true)
        {
            int randomColumnIndex = Random.Range(1, computerGrid.numberOfColumns + 1);
            int randomRowIndex = Random.Range(1, computerGrid.numberOfRows + 1);
            SinglePiece randomPiece = computerGrid.PieceArray[randomColumnIndex, randomRowIndex];
            foreach (Vector2 dir in Directions.AllDirections)
            {
                if (computerGrid.PieceArray[(int)randomPiece.Location.x + (int)dir.x, (int)randomPiece.Location.y + (int)dir.y].PieceType != SinglePieceType.None)
                {
                    SinglePiece otherPiece = computerGrid.PieceArray[(int)randomPiece.Location.x + (int)dir.x, (int)randomPiece.Location.y + (int)dir.y];
                    SwapPieces(randomPiece.Location, otherPiece.Location);
                    if (Matching.CheckForMatch(computerGrid.PieceArray[(int)randomPiece.Location.x, (int)randomPiece.Location.y], computerGrid.PieceArray, Directions.AllDirections) ||
                        Matching.CheckForMatch(computerGrid.PieceArray[(int)otherPiece.Location.x, (int)otherPiece.Location.y], computerGrid.PieceArray, Directions.AllDirections)) // If either matches
                    {
                        SwapPieces(otherPiece.Location, randomPiece.Location);
                        matchingDir = dir;
                        return randomPiece;
                    }
                    else
                    {
                        SwapPieces(otherPiece.Location, randomPiece.Location);
                    }
                }                
            }
        }
    }

    private void SwapPieces(Vector2 pieceOneLoc, Vector2 pieceTwoLoc)
    {
        SinglePiece pieceOne = computerGrid.PieceArray[(int)pieceOneLoc.x, (int)pieceOneLoc.y];
        computerGrid.PieceArray[(int)pieceOneLoc.x, (int)pieceOneLoc.y] = computerGrid.PieceArray[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y];
        computerGrid.PieceArray[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y] = pieceOne;
        computerGrid.ResetPieceLocation(pieceOneLoc);
        computerGrid.ResetPieceLocation(pieceTwoLoc);
    }
}

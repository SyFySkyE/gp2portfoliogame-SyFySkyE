using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using UnityEngine;

public class AI
{
    private Grid computerGrid;
    private SinglePiece pieceSelected;

    public AI(Grid enemyGrid)
    {
        computerGrid = new Grid(enemyGrid.numberOfColumns, enemyGrid.numberOfRows);
        this.computerGrid = enemyGrid;
    }

    public SinglePiece SelectNextPiece()
    {
        SinglePiece matchedPiece = null;
        Vector2 matchDir = Vector2.zero;

        if (pieceSelected == null)
        {
            pieceSelected = SelectInitialPiece(matchedPiece, out matchDir);
            matchedPiece = pieceSelected;
        }
        else
        {
            pieceSelected = computerGrid.PieceArray[(int)pieceSelected.Location.x + (int)matchDir.x, (int)pieceSelected.Location.y + (int)matchDir.y];
            matchedPiece = pieceSelected;
            pieceSelected = null;
        }

        return matchedPiece;
    }

    private SinglePiece SelectInitialPiece(SinglePiece currentPiece, out Vector2 matchingDir)
    {
        while (true)
        {
            int randomColumn = Random.Range(1, computerGrid.numberOfColumns + 1);
            int randomRow = Random.Range(1, computerGrid.numberOfRows + 1);
            currentPiece = computerGrid.PieceArray[randomColumn, randomRow];
            foreach (Vector2 dir in Directions.AllDirections)
            {
                SwapPieces(currentPiece, dir);
                if (Matching.CheckForMatch(currentPiece, computerGrid.PieceArray, Directions.AllDirections))
                {
                    SwapPieces(currentPiece, -dir);
                    Debug.Log($"{currentPiece.PieceType} at {currentPiece.Location} moved {dir}");
                    matchingDir = dir;
                    return currentPiece;
                }
            }
        }
    }

    private void SwapPieces(SinglePiece currentPiece, Vector2 dir)
    {
        if (computerGrid.PieceArray[(int)currentPiece.Location.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y].PieceType == SinglePieceType.None)
        {
            return;
        }
        else
        {
            SinglePiece firstPiece = currentPiece;
            SinglePiece otherPiece = computerGrid.PieceArray[(int)currentPiece.Location.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y];
            computerGrid.PieceArray[(int)currentPiece.Location.x, (int)currentPiece.Location.y] = otherPiece;
            computerGrid.PieceArray[(int)otherPiece.Location.x, (int)otherPiece.Location.y] = firstPiece;
            computerGrid.ResetPieceLocation(otherPiece.Location);
            computerGrid.ResetPieceLocation(currentPiece.Location);
        }
    }
}

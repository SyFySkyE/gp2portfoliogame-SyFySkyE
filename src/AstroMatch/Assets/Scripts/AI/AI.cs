using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    private Grid computerGrid;

    public AI(Grid enemyGrid)
    {
        computerGrid = new Grid(enemyGrid.numberOfColumns, enemyGrid.numberOfRows);
        this.computerGrid = enemyGrid;
    }

    public SinglePiece SelectNextPiece(out Vector2 direction)
    {
        direction = Vector2.zero;
        SinglePiece matchedPiece;
        

        while (true)
        {
            int randomColumn = Random.Range(1, computerGrid.numberOfColumns + 1);
            int randomRow = Random.Range(1, computerGrid.numberOfRows + 1);
            matchedPiece = computerGrid.PieceArray[randomColumn, randomRow];
            foreach (Vector2 dir in Directions.RecursiveDirections)
            {
                if (computerGrid.PieceArray[(int)matchedPiece.Location.x + (int)dir.x, (int)matchedPiece.Location.y + (int)dir.y].PieceType != SinglePieceType.None)
                {
                    matchedPiece.Location += dir;
                    if (Matching.CheckForMatch(matchedPiece, computerGrid.PieceArray, Directions.RecursiveDirections))
                    {
                        direction = dir;
                        Debug.Log($"Piece type: {matchedPiece.PieceType} at { matchedPiece.Location - dir} will match if moved {dir}");
                        matchedPiece.Location -= dir;
                        return matchedPiece;
                    }
                    matchedPiece.Location -= dir;
                }
            }
        }

        //foreach(SinglePiece piece in computerGrid.PieceArray)
        //{
        //    if (piece.PieceType != SinglePieceType.None)
        //    {
        //        foreach (Vector2 dir in Directions.RecursiveDirections)
        //        {
        //            if (computerGrid.PieceArray[(int)piece.Location.x + (int)dir.x, (int)piece.Location.y + (int)dir.y].PieceType != SinglePieceType.None)
        //            {
        //                piece.Location += dir;
        //                if (Matching.CheckForMatch(piece, computerGrid.PieceArray, Directions.AllDirections))
        //                {
        //                    direction = dir;

        //                    Debug.Log(piece.Location - dir + " will match if moved " + dir);
        //                    piece.Location -= dir;
        //                    return piece;
        //                }
        //                piece.Location -= dir;
        //            }

        //        }
        //    }            
        //}

    }
}

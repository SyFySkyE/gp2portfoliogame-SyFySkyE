using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Matching 
{
    public static bool CheckInitialMatch(SinglePiece currentPiece, SinglePiece[,] pieceArray, Vector2[] directionsToCheck)
    {
        foreach(Vector2 dir in directionsToCheck)
        {
            if (pieceArray[(int)currentPiece.Location.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y].PieceType == currentPiece.PieceType)
            {
                if (pieceArray[(int)currentPiece.Location.x + (int)dir.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y + (int)dir.y].PieceType == currentPiece.PieceType)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool CheckForMatch(SinglePiece currentPiece, SinglePiece[,] pieceArray, Vector2[] directionsToCheck)
    {
        foreach (Vector2 dir in directionsToCheck)
        {
            if (pieceArray[(int)currentPiece.Location.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y].PieceType == currentPiece.PieceType)
            {
                if (pieceArray[(int)currentPiece.Location.x + (int)dir.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y + (int)dir.y].PieceType == currentPiece.PieceType)
                {
                    return true;
                }
                else if (pieceArray[(int)currentPiece.Location.x - (int)dir.x, (int)currentPiece.Location.y - (int)dir.y].PieceType == currentPiece.PieceType)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

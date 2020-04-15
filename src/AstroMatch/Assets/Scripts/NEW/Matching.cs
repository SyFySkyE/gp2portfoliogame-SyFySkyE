using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Matching 
{
    public static int matchMin = 3;

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

    public static List<SinglePiece> GetConnectedPieces(SinglePiece currentPiece, SinglePiece[,] pieceArray, Vector2[] directionsToCheck)
    {
        List<SinglePiece> connectedPieces = new List<SinglePiece>();
        foreach(Vector2 dir in directionsToCheck)
        {
            if (pieceArray[(int)currentPiece.Location.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y].PieceType == currentPiece.PieceType)
            {
                // By here, we have a two match
                connectedPieces.Add(currentPiece);
                connectedPieces.Add(pieceArray[(int)currentPiece.Location.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y]); // Can we add this logic to lower?
                Vector2 matchDir = dir + dir;
                if (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType)
                {
                    // By here, we have at least a three match, from one end to the other
                    connectedPieces.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                    matchDir += dir;
                    while (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType) // Keep adding peices along the direction
                    {
                        connectedPieces.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                        matchDir += dir;
                    }
                }

                matchDir = -dir; // If we math a peice in the middle of the three match or higher, we'll need to reverse the direction to collect those pieces as well

                if (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType)
                {
                    // By here, we have at least a three match, from one end to the other
                    connectedPieces.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                    matchDir -= dir;
                    while (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType) // Keep adding peices along the direction
                    {
                        connectedPieces.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                        matchDir -= dir;
                    }
                }
            }

            if (connectedPieces.Count >= matchMin)
            {
                return connectedPieces;
            }
        }
        return null;
    }
    
}

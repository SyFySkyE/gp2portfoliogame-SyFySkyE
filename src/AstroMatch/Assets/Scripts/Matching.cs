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
                List<SinglePiece> pieceMatchDir = new List<SinglePiece>();
                // By here, we have a two match                              
                Vector2 matchDir = dir + dir;
                pieceMatchDir.Add(currentPiece);
                pieceMatchDir.Add(pieceArray[(int)currentPiece.Location.x + (int)dir.x, (int)currentPiece.Location.y + (int)dir.y]);
                if (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType)
                {
                    // By here, we have at least a three match, from one end to the other                    
                    pieceMatchDir.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                    matchDir += dir;
                    while (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType) // Keep adding peices along the direction
                    {
                        pieceMatchDir.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                        matchDir += dir;
                    }
                }

                matchDir = -dir; // If we macth a peice in the middle of the three match or higher, we'll need to reverse the direction to collect those pieces as well

                if (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType)
                {
                    // By here, we have at least a three match, from one end to the other
                    pieceMatchDir.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                    matchDir -= dir;
                    while (pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y].PieceType == currentPiece.PieceType) // Keep adding peices along the direction
                    {
                        pieceMatchDir.Add(pieceArray[(int)currentPiece.Location.x + (int)matchDir.x, (int)currentPiece.Location.y + (int)matchDir.y]);
                        matchDir -= dir;
                    }
                }

                if (pieceMatchDir.Count >= matchMin)
                {
                    foreach (SinglePiece piece in pieceMatchDir)
                    {
                        connectedPieces.Add(piece);
                    }
                }
            }            
        }

        if (connectedPieces.Count >= matchMin)
        {            
            return connectedPieces;
        }
        else
        {
            Debug.LogWarning("Matching returned null!");
            return null;
        }
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match 
{
    private static int matchMinimum = 3; // TODO Break up large if lines

    public static List<Cell> GetConnectedCells(Cell currentCell, Cell[,] cellArray, Vector2[] directionsToCheck)
    {
        List<Cell> connectedCells = null;
        foreach (Vector2 dir in directionsToCheck)
        {
            if (cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell != null && cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
            {

                // By here, we have a two match
                connectedCells = new List<Cell>(); // TODO Do we hafta initialize it only when we have a two match?
                connectedCells.Add(currentCell);
                connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]);
                Vector2 matchDir = dir + dir;
                if (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell != null && cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // Match three, from one end
                {                    
                    connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                    matchDir += dir;
                    Debug.Log("1");
                    while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell != null && cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
                    {
                        connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                        matchDir += dir;
                    }                    
                }

                matchDir = -dir;

                if (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell != null && cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // Match three, from one end
                {
                    connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                    matchDir -= dir;
                    while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell != null && cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
                    {
                        connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                        matchDir -= dir;
                    }
                }
            }

            if (connectedCells != null)
            {
                if (connectedCells.Count >= matchMinimum)
                {
                    return connectedCells;
                }
            }            
        }        
        return null; // If we reach here, no match was made
    }

    //public static List<Cell> GetConnectedCells(Cell currentCell, Cell[,] cellArray, Vector2[] directionsToCheck) 
    //{
    //    List<Cell> ConnectedCells = new List<Cell>();
    //    foreach (Vector2 dir in directionsToCheck)
    //    {
    //        try
    //        {                
    //            if (cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // TODO this is dirty. Can we clean this up? // If this is true, then we got a two-match at least
    //            {
    //                Vector2 matchDir = dir + dir;
    //                if (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // If we get here, there's at least a three match
    //                {
    //                    ConnectedCells.Add(currentCell);
    //                    ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]);
    //                }                    
                        
    //                while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) 
    //                {
    //                    ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
    //                    matchDir += dir;
    //                }

    //                // matchDir -= dir;

    //                matchDir = -dir;
    //                while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
    //                {
    //                    ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
    //                    matchDir -= dir;
    //                }

    //                // matchDir += dir;

    //                return ConnectedCells;
    //            }
    //        }
    //        catch // !!! When the checked array is out of index, it skips to here 
    //        {
    //            if (ConnectedCells.Count == 0)
    //            {
    //                continue;
    //            }
    //            else
    //            {
    //                return ConnectedCells;
    //            }
                
    //            // Out of bounds of the array, or that array cell hasn't been initialized
    //            // TODO Don't need this so find a way to not do a try catch
    //        }
    //    }
    //    return ConnectedCells;
    //}

    //public static List<Cell> GetLConnectedCells(Cell currentCell, Vector2 matchingDir)
    //{
    //    List<Cell> ConnectedCells = new List<Cell>();
    //    foreach (Vector2 dir in Directions.AllDirections)
    //    {
    //        if (matchingDir == dir || -matchingDir == dir)
    //        {
    //            break;
    //        }
    //        try
    //        {
    //            Vector2 matchDir = dir + dir;
    //            if (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // TODO this is dirty. Can we clean this up? // If this is true, then we got a two-match at least
    //            {                    
    //                ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]);
    //                while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
    //                {
    //                    ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
    //                    matchDir += dir;
    //                }

    //                matchDir = -dir;
    //                while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
    //                {
    //                    ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
    //                    matchDir -= dir;
    //                }

    //                return ConnectedCells;
    //            }
    //        }
    //        catch // !!! When the checked array is out of index, it skips to here 
    //        {
    //            if (ConnectedCells.Count == 0)
    //            {
    //                continue;
    //            }
    //            else
    //            {
    //                return ConnectedCells;
    //            }

    //            // Out of bounds of the array, or that array cell hasn't been initialized
    //            // TODO Don't need this so find a way to not do a try catch
    //        }
    //    }
    //    return ConnectedCells;
    //}

    public static bool CheckForInitialMatch(Cell currentCell, Cell[,] cellArray, Vector2[] directionsToCheck)
    {
        foreach (Vector2 dir in directionsToCheck)
        {
            try
            {
                if (cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // TODO this is dirty. Can we clean this up? // If this is true, then we got a two-match at least
                {
                    if (cellArray[(int)currentCell.CellLocation.x + (int)dir.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y + (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // TODO this is dirty. Can we clean this up? // Three Match
                    {
                        return true;
                    }
                    else if (cellArray[(int)currentCell.CellLocation.x - (int)dir.x, (int)currentCell.CellLocation.y - (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                 
            }
        }
        return false;
    }
}
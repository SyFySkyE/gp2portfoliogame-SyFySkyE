using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match 
{
    public static List<Cell> GetConnectedCells(Cell currentCell, Cell[,] cellArray, Vector2[] directionsToCheck) // TODO Shouldn't have to constantly send the array over each time!
    {
        List<Cell> ConnectedCells = new List<Cell>();
        foreach (Vector2 dir in directionsToCheck)
        {
            try
            {
                if (cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // TODO this is dirty. Can we clean this up? // If this is true, then we got a two-match at least
                {
                    Vector2 matchDir = dir + dir;
                    ConnectedCells.Add(currentCell);
                    ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]);
                    while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // TODO this is dirty. Can we clean this up? // Three Match
                    {
                        ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                        matchDir += dir;
                    }
                    
                    // Would have to be just -dir to work WHy not just move over from the newly found connected cell?
                    matchDir = -dir;
                    while (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
                    {
                        ConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                        matchDir -= dir;
                    }
                    
                    return ConnectedCells;
                }
            }
            catch // !!! When the checked array is out of index, it skips to here 
            {
                if (ConnectedCells.Count == 0)
                {
                    continue;
                }
                else
                {
                    return ConnectedCells;
                }
                
                // Out of bounds of the array, or that array cell hasn't been initialized
                // TODO Don't need this so find a way to not do a try catch
            }
        }
        return ConnectedCells;
    }    

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
                 // The check for match keeps matching two at the top
            }
        }
        return false;
    }
}

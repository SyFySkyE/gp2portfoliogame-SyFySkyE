using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match 
{
    public static bool CheckForInitialMatch(Cell currentCell, Cell[,] cellArray, Vector2[] directionsToCheck) // TODO Shouldn't have to constantly send the array over each time!
    {
        foreach (Vector2 dir in directionsToCheck)
        {
            try
            {
                if (cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell.GetType() == currentCell.PieceInCell.GetType()) // TODO this is dirty. Can we clean this up? 
                {
                    if (cellArray[(int)currentCell.CellLocation.x + (int)dir.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y + (int)dir.y].PieceInCell.GetType() == currentCell.PieceInCell.GetType()) // TODO this is dirty. Can we clean this up?
                    {

                        return true; // Only gets here if three cells in the same direction have the same piece
                    }
                    else if (cellArray[(int)currentCell.CellLocation.x - (int)dir.x, (int)currentCell.CellLocation.y - (int)dir.y].PieceInCell.GetType() == currentCell.PieceInCell.GetType())
                    {

                        return true; // Only gets here if moving middle piece nets a match
                    }
                }
            }
            catch
            {
                // Out of bounds of the array, or that array cell hasn't been initialized
                // TODO Don't need this so find a way to not do a try catch
            }
        }
        return false;
    }
}

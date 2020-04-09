using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match 
{
    private static int matchMinimum = 3; 

    public static List<Cell> GetConnectedCells(Cell currentCell, Cell[,] cellArray, Vector2[] directionsToCheck)
    {
        bool isCornerPiece = false;
        List<Cell> connectedCells = null;
        foreach (Vector2 dir in directionsToCheck)
        {
            if (!IsCellPieceNull(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]) && 
                cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
            {
                // By here, we have a two match
                connectedCells = new List<Cell>(); // TODO Do we hafta initialize it only when we have a two match?
                connectedCells.Add(currentCell);
                connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]);
                Vector2 matchDir = dir + dir;

                if (!IsCellPieceNull(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y] )&& 
                    cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // Match three, from one end
                {
                    isCornerPiece = true;
                    connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                    matchDir += dir; 
                    while (!IsCellPieceNull(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y] ) && 
                        cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
                    {
                        connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                        matchDir += dir;
                    }                
                }

                matchDir = -dir;

                if (!IsCellPieceNull(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]) && 
                    cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType) // Match three, from the middle
                {
                    isCornerPiece = false;
                    connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                    matchDir -= dir;
                    while (!cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y] && 
                        cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)
                    {
                        connectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                        matchDir -= dir;
                    }
                }

                if (isCornerPiece)
                {
                    foreach (Cell cell in GetLShapedConnectedCells(currentCell, cellArray, dir))
                    {
                        connectedCells.Add(cell);
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

    private static List<Cell> GetLShapedConnectedCells(Cell currentCell, Cell[,] cellArray, Vector2 matchingDir)
    {
        List<Cell> lConnectedCells = new List<Cell>();
        Vector2[] perpendicularDir = new Vector2[2];
        if (matchingDir.x != 0)
        {
            perpendicularDir[0] = Vector2.up;
            perpendicularDir[1] = Vector2.down;            
        }
        else if (matchingDir.y != 0)
        {
            perpendicularDir[0] = Vector2.left;
            perpendicularDir[1] = Vector2.right;
        }


        foreach (Vector2 dir in perpendicularDir)
        {
            if (CheckForInitialMatch(currentCell, cellArray, perpendicularDir))
            {
                if (!IsCellPieceNull(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]) && (cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)) // Match Two
                {                    
                    Vector2 matchDir = dir + dir;
                    if (!IsCellPieceNull(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]) &&
                        (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType)) // Match Three
                    {
                        lConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)dir.x, (int)currentCell.CellLocation.y + (int)dir.y]);
                        while (!IsCellPieceNull(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]) &&
                        (cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y].PieceInCell.PieceCurrentType == currentCell.PieceInCell.PieceCurrentType))
                        {
                            lConnectedCells.Add(cellArray[(int)currentCell.CellLocation.x + (int)matchDir.x, (int)currentCell.CellLocation.y + (int)matchDir.y]);
                            matchDir += dir;
                        }
                        break;
                    }
                }
            }
        }
        return lConnectedCells;
    }

    public static bool IsCellPieceNull(Cell cell)
    {
        if (cell.PieceInCell == null) return true;
        else return false;        
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
                 
            }
        }
        return false;
    }
}
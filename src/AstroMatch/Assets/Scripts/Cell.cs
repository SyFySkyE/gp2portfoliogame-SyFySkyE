using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2 CellLocation;
    public IMatachable PieceInCell;
    private Vector2 initialLocation;
    private IMatachable initialPiece;

    public Cell(int column, int row, IMatachable piece)
    {
        this.CellLocation = initialLocation = new Vector2(column, row);
        this.PieceInCell = initialPiece = piece;
    }

    public void SwapCells(Cell otherCell)
    {
        this.CellLocation = otherCell.CellLocation;
        otherCell.CellLocation = this.initialLocation;
        this.PieceInCell = otherCell.PieceInCell;
        otherCell.PieceInCell = this.initialPiece;
    }
}

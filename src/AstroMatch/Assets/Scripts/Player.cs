using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("This player's grid")]
    [SerializeField] private PlayGrid playerGrid;

    private Cell cellSelected;    

    #region EventSubscriptions
    private void OnEnable()
    {
        Cell.OnSelectThisCell += OnSelectThisCell;
    }
    private void OnDisable()
    {
        Cell.OnSelectThisCell -= OnSelectThisCell;
    }
    #endregion

    private void OnSelectThisCell(Cell clickedCell)
    {
        if (cellSelected == null)
        {
            SelectPiece(clickedCell);
        }
        else
        {
            AttemptToSwap(clickedCell);
        }        
    }

    private void SelectPiece(Cell clickedCell)
    {
        cellSelected = clickedCell;
    }

    private void AttemptToSwap(Cell clickedCell)
    {
        foreach (Vector2 direction in Directions.AllDirections)
        {
            if ((this.cellSelected.CellLocation) + direction == clickedCell.CellLocation)
            {                
                playerGrid.SwapPieces(cellSelected, clickedCell, false);
                cellSelected = null;
                return;
            }
        }
        // If it reaches this far, player has chosen a piece they cannot swap with
        cellSelected = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!playerGrid) throw new System.Exception($"{this.name} player does NOT have a play grid assigned! Please assign it in the inspector. - CG");
    }
}

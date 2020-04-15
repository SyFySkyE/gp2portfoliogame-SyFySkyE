using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNew : MonoBehaviour
{
    [Header("The player's grid")]
    [SerializeField] private UnityGrid playerGrid;

    //private SinglePiece pieceSelected;

    //private void OnEnable()
    //{
    //    UnityPiece.OnPieceSelect += UnityPiece_OnPieceSelect;
    //}

    //private void UnityPiece_OnPieceSelect(Vector2 cellLocation)
    //{
    //    if (pieceSelected == null)
    //    {
    //        //pieceSelected = playerGrid.PieceArray[(int)cellLocation.x, (int)cellLocation.y];
    //    }
    //    else
    //    {
    //        AttemptToSwap(cellLocation);
    //    }
    //}

    //private void AttemptToSwap(Vector2 cellLocation)
    //{
    //    foreach (Vector2 dir in Directions.AllDirections)
    //    {
    //        if ((this.pieceSelected.Location) + dir == playerGrid.PieceArray[(int)cellLocation.x, (int)cellLocation.y].Location)
    //        {
    //            pieceSelected = null;
    //            Debug.Log("Clicked a good peice");
    //            return;
    //        }
    //    }

    //    pieceSelected = null;
    //}
}

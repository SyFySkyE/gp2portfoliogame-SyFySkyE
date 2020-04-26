using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : Command
{
    protected UnityPiece selectedPiece;
    protected Grid previousGridState;

    public SelectPiece(UnityPiece clickedPiece)
    {
        this.selectedPiece = clickedPiece;
    }

    public override void Execute()
    {
        Debug.Log("Executed");
        selectedPiece.SelectPiece();
    }
}

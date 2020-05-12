using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPiece : Command
{
    protected UnityPiece selectedPiece;    

    public SelectPiece(UnityPiece clickedPiece)
    {
        this.selectedPiece = clickedPiece;
    }

    public override void Execute()
    {
        if (selectedPiece != null)
        {
            selectedPiece.SelectPiece();
        }        
    }

    public override void Undo()
    {
        base.Undo();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPieceCommand : UnityCommand
{
    protected UnityPiece thisPiece;    

    public override void Execute()
    {
        this.thisPiece.SelectPiece();
    }
}

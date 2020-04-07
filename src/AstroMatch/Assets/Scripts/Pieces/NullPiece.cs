using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullPiece : Piece
{
    public override void SetupPiece()
    {
        this.isNull = true;
        this.currentState = PieceState.None;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullPiece : Piece
{
    protected override void Start()
    {
        this.pieceType = PieceType.None;
        this.currentState = PieceState.None;
        base.Start();
    }
}

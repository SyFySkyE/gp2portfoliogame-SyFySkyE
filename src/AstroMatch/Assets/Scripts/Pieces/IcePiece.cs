using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePiece : Piece
{
    protected override void Start()
    {
        this.pieceType = PieceType.Ice;
        this.currentState = PieceState.Normal;
        base.Start();
    }
}

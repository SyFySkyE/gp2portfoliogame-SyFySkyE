using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPiece : Piece
{
    protected override void Start()
    {
        this.pieceType = PieceType.Sun;
        this.currentState = PieceState.Sun;
        base.Start();
    }
}

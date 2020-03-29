using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePiece : Piece
{
    protected override void Start()
    {
        this.pieceType = PieceType.Purple;
        this.currentState = PieceState.Normal;
        base.Start();
    }
}

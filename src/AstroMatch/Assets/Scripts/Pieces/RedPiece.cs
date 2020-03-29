using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPiece : Piece
{
    protected override void Start()
    {
        this.pieceType = PieceType.Red;
        this.currentState = PieceState.Normal;
        base.Start();
    }
}

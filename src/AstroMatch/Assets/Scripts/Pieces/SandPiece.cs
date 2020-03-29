using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPiece : Piece
{
    protected override void Start()
    {
        this.pieceType = PieceType.Sand;
        this.currentState = PieceState.Normal;
        base.Start();
    }
}

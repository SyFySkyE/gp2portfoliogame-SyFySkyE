using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPiece : Piece
{
    protected override void Start()
    {
        this.pieceType = PieceType.Water;
        this.currentState = PieceState.Normal;
        base.Start();
    }
}

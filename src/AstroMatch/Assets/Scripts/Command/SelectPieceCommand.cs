using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPieceCommand : UnityCommand
{
    private UnityPiece thisPiece;

    private void Start()
    {
        thisPiece = GetComponent<UnityPiece>();
    }

    public override void Execute()
    {
        this.thisPiece.SelectPiece();
    }
}

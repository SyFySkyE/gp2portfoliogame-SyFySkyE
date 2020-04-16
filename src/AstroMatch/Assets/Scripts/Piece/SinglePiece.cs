using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SinglePieceType
{
    None,
    Ice,
    Water,
    Red,
    Purple,
    Sand
}

public class SinglePiece
{
    public SinglePieceType PieceType { get; set; }
    public Vector2 Location { get; set; }
    
    public SinglePiece(float x, float y, bool isNull)
    {
        Location = new Vector2(x, y);
        if (isNull)
        {
            this.PieceType = SinglePieceType.None;
        }
        else
        {
            RandomizeType();
        }
    }

    public void RandomizeType()
    {
        this.PieceType = (SinglePieceType)Random.Range(1, System.Enum.GetValues(typeof(SinglePieceType)).Length);
    }
}

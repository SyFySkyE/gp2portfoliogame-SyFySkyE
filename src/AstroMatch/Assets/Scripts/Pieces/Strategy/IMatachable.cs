using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatachable 
{
    PieceState PieceCurrentState { get; }
    PieceType PieceCurrentType { get; }
    Sprite PieceImage { get; }
    RectTransform PieceRectTransform { get; }
    void SwitchPlace(Vector2 dir);
    void Match();
}

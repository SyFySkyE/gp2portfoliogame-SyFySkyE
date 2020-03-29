using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatachable 
{
    Sprite PieceImage { get; }
    RectTransform PieceRectTransform { get; }
    void SwitchPlace(Vector2 dir);
    void Match();
}

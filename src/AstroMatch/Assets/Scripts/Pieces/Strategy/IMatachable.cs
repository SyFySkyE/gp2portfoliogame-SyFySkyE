using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatachable 
{
    PieceState PieceCurrentState { get; set; }
    PieceState InitialPieceState { get; }
    PieceType PieceCurrentType { get; set; }
    PieceType InitialPieceType { get; }
    Sprite PieceImage { get; }
    Vector2 CellLocation { get; set; }    
    Vector2 InitialCellLocation { get; }
    RectTransform PieceRectTransform { get; }
    void SwitchPlace(Vector2 dir);
    void Match();
    string Log();
}

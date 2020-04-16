using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceMouseSelectCommand : SelectPieceCommand, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        this.Execute();
    }
}

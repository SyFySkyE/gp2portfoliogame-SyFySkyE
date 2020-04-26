using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceMouseSelectCommand : SelectPieceCommand, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        this.thisPiece = eventData.pointerEnter.GetComponent<UnityPiece>();
        Execute();
    }


    public override void Execute()
    {
        base.Execute();
    }
}

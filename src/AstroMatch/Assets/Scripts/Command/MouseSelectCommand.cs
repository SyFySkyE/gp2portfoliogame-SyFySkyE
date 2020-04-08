using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSelectCommand : SelectCellCommand, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        this.Execute();
    }
}

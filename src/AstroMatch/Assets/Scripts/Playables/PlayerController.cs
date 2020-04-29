using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : UnityBasePlayer, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            SelectPiece newCommand = new SelectPiece(eventData.pointerEnter.GetComponent<UnityPiece>());
            commandProcessor.AddNewCommand(newCommand);
        }
    }
}

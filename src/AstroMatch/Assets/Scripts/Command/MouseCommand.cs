using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCommand : MonoBehaviour, IPointerClickHandler
{
    private Cell currentCell;

    public void OnPointerClick(PointerEventData eventData)
    {
        Execute();
    }

    private void Execute()
    {
        currentCell.SelectCell();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentCell = GetComponent<Cell>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCellCommand : UnityCommand
{
    private Cell thisCell;

    // Start is called before the first frame update
    void Start()
    {
        thisCell = GetComponent<Cell>();
    }

    public override void Execute()
    {
        thisCell.SelectCell();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPop : MonoBehaviour
{
    private TMPro.TextMeshProUGUI numCanvasElement;
    private Animator numPopAnim;
    private UnityGrid grid;    

     // Start is called before the first frame update
    void Start()
    {
        grid = GetComponentInParent<UnityGrid>();
        grid.OnCellsMatched += Grid_OnCellsMatched;
        numPopAnim = GetComponent<Animator>();
        numCanvasElement = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Grid_OnCellsMatched(int numberOfCellsMatched)
    {
        PopNumber(numberOfCellsMatched);
    }

    private void PopNumber(int value)
    {
        numCanvasElement.text = $"+{value}";
        numPopAnim.SetTrigger("Pop");
    }
}

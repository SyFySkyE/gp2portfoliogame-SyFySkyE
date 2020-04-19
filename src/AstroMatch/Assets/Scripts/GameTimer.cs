using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Initial Time, in seconds")]
    [SerializeField] private float timer = 30;

    [Header("TODO Make this its own game manager object")]
    [SerializeField] private GameObject gameOverCanvas;

    private UnityGrid playerGrid;
    private TMPro.TextMeshProUGUI timeText;

    private bool isInProgress = true;
 
    // Start is called before the first frame update
    void Start()
    {
        playerGrid = GetComponent<UnityGrid>();
        playerGrid.OnCellsMatched += PlayerGrid_OnCellsMatched;
        timeText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void PlayerGrid_OnCellsMatched(int cellsMatched)
    {
        timer += cellsMatched;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInProgress)
        {
            if (timer <= Mathf.Epsilon)
            {
                StopGame(); // TODO Handle this with a separate GameManager obj
            }
            else
            {
                timer -= Time.deltaTime;
                timeText.text = timer.ToString("F2");
            }
        }       
    }

    private void StopGame()
    {
        playerGrid.enabled = false;
        isInProgress = false;
        timer = 0;
        timeText.text = timer.ToString("F2");
        gameOverCanvas.SetActive(true);
    }
}

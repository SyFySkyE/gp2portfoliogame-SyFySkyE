using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Initial Time, in seconds")]
    [SerializeField] private float timer = 30;

    [Header("When opponent makes a match, subtract this timer multiplied by this amount")]
    [SerializeField] private float subtractMultiplier = 0.7f;

    private UnityGrid playerGrid;
    private TMPro.TextMeshProUGUI timeText;

    public event System.Action<int> OnPlayerMatched;

    private bool isInProgress = true;
    public event System.Action OnGameLose;
 
    // Start is called before the first frame update
    void Start()
    {
        playerGrid = GetComponent<UnityGrid>();
        playerGrid.OnCellsMatched += PlayerGrid_OnCellsMatched;
        timeText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void PlayerGrid_OnCellsMatched(int cellsMatched)
    {
        OnPlayerMatched(cellsMatched);
        timer += cellsMatched;
    }

    public void SubtractTime(int opponentCellsMatched)
    {
        timer -= opponentCellsMatched * subtractMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInProgress)
        {
            if (timer <= Mathf.Epsilon)
            {
                StopGame(); 
                OnGameLose();
                this.gameObject.SetActive(false);
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
    }

    public void Reset()
    {
        Start();
    }
}

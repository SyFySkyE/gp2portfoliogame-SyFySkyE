using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("Initial Time, in seconds")]
    [SerializeField] private float timer = 30;

    [Header("When opponent makes a match, subtract this timer multiplied by this amount")]
    [SerializeField] private float subtractMultiplier = 0.7f;

    [SerializeField] private float subtractMultiplierIncrement = 0.1f;
    [SerializeField] private float maxSubtractMultiplier = 1.1f;

    private UnityGrid playerGrid;
    private Slider timeSlider;
    private Image timeSliderFillRect;
    private TMPro.TextMeshProUGUI timeText;
    public event System.Action<int> OnLose;

    private bool isInProgress = true;
 
    // Start is called before the first frame update
    void Start()
    {
        timeSlider = GetComponentInChildren<Slider>();
        timeSlider.maxValue = timer;                
        timeSliderFillRect = timeSlider.fillRect.GetComponent<Image>();
        CalculateSlider();
        playerGrid = GetComponent<UnityGrid>();
        playerGrid.OnCellsMatched += PlayerGrid_OnCellsMatched;
        timeText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void PlayerGrid_OnCellsMatched(int cellsMatched, int userID)
    {
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
            }
            else
            {
                timer -= Time.deltaTime;
                CalculateSlider();
                timeText.text = timer.ToString("F2");
            }
        }       
    }

    private void CalculateSlider()
    {
        timeSlider.value = timer;
        if (timeSlider.value > timeSlider.maxValue / 2)
        {
            timeSliderFillRect.color = new Color(0, 255, 0, .25f); // Green
        }
        else if (timeSlider.value < timeSlider.maxValue / 2 && timeSlider.value > timeSlider.maxValue / 4)
        {
            timeSliderFillRect.color = new Color(255, 255, 0, .25f); // Yellow
        }
        else
        {
            timeSliderFillRect.color = new Color(255, 0, 0, .25f); // Red
        }
    }

    private void StopGame()
    {
        playerGrid.enabled = false;
        isInProgress = false;
        timer = 0;
        timeText.text = timer.ToString("F2");
        OnLose?.Invoke(playerGrid.UserID);
    }

    public void Reset()
    {
        Start();
    }

    public void IncrementSubtractMultiplier()
    {
        if (this.subtractMultiplier <= maxSubtractMultiplier)
        {
            subtractMultiplier += 0.1f;
        }
    }
}

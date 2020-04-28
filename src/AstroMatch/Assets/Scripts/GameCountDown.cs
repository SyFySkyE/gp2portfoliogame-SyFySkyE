using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCountDown : MonoBehaviour
{
    [SerializeField] private Image blockImage;
    [SerializeField] private TextMeshProUGUI countdownLabel;

    private float startTimer;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = 3;
        if (blockImage != null)
        {
            blockImage.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (blockImage != null && countdownLabel != null)
        {
            if (startTimer >= 0)
            {
                startTimer -= Time.deltaTime;
                countdownLabel.text = startTimer.ToString("F0");
                blockImage.color = Color.Lerp(blockImage.color, Color.clear, Time.deltaTime);
            }
            else
            {
                countdownLabel.text = string.Empty;
                blockImage.enabled = false;
                this.gameObject.SetActive(false);
            }
        }        
    }
}

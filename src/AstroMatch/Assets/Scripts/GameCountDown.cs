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
    private int currentSecond;

    // Start is called before the first frame update
    void Start()
    {
        startTimer = 3;
        if (blockImage != null)
        {
            blockImage.color = Color.black;
        }
        SoundPlayer.Instance.PlayOneShot(SoundClips.Countdown);
    }

    // Update is called once per frame
    void Update()
    {
        if (blockImage != null && countdownLabel != null)
        {
            if (startTimer >= 0)
            {
                string currentDownLabelText = countdownLabel.text;
                startTimer -= Time.deltaTime;
                countdownLabel.text = startTimer.ToString("F0");
                if (countdownLabel.text != currentDownLabelText)
                {
                    SoundPlayer.Instance.PlayOneShot(SoundClips.Countdown);
                }
                
                blockImage.color = Color.Lerp(blockImage.color, Color.clear, Time.deltaTime);
            }
            else
            {
                SoundPlayer.Instance.PlayOneShot(SoundClips.Countdown);
                this.gameObject.SetActive(false);
                this.enabled = false;                
            }
        }        
    }

    private void PlayCountdownSfx()
    {
        if (currentSecond == (int)startTimer)
        {
            return;
        }
        else
        {
            currentSecond = (int)startTimer;
            
        }
    }

    public void Reset()
    {
        Start();
    }
}

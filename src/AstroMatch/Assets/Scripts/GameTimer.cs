using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("Initial Time, in seconds")]
    [SerializeField] private float timer = 30;

    private TMPro.TextMeshProUGUI timeText;
 
    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timeText.text = timer.ToString("F2");
    }
}

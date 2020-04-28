using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    class GameManager : MonoBehaviour
{
    [SerializeField] private GameTimer playerOne;
    [SerializeField] private GameTimer playerTwo;
    [SerializeField] private GameObject playerWinCanvas;
    [SerializeField] private GameObject computerWinCanvas;

    // Start is called before the first frame update
    void Start()
    {
        playerOne.OnGameLose += PlayerOne_OnGameLose;
        playerTwo.OnGameLose += PlayerTwo_OnGameLose;
    }

    private void PlayerTwo_OnGameLose()
    {
        Time.timeScale = 0;
        playerWinCanvas.SetActive(true);
    }

    private void PlayerOne_OnGameLose()
    {
        Time.timeScale = 0;
        computerWinCanvas.SetActive(true);
    }   
}

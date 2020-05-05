using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    class GameManager : MonoBehaviour
{
    [Header("Player parameters")]
    [SerializeField] private GameTimer playerOne;
    [SerializeField] private GameTimer playerTwo;
    [SerializeField] private GameObject playerWinCanvas;
    [SerializeField] private GameObject computerWinCanvas;
    [SerializeField] private GameCountDown countdown;

    // Start is called before the first frame update
    void Start()
    {
        playerOne.OnGameLose += PlayerOne_OnGameLose;
        playerTwo.OnGameLose += PlayerTwo_OnGameLose;
        playerOne.OnPlayerMatched += PlayerOne_OnPlayerMatched;
        playerTwo.OnPlayerMatched += PlayerTwo_OnPlayerMatched;
    }

    private void PlayerTwo_OnPlayerMatched(int cellsMatched)
    {
        playerOne.SubtractTime(cellsMatched);
    }

    private void PlayerOne_OnPlayerMatched(int cellsMatched)
    {
        playerTwo.SubtractTime(cellsMatched);
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

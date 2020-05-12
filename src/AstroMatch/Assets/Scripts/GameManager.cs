using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private List<UnityGrid> players;    

    [SerializeField] private GameObject playAgainCanvas;
    [Header("Game Parameters")]
    [SerializeField] private GameCountDown countdown;
    [Header("Number of moves before speed of matching and AI increases")]
    [SerializeField] private float secondsBeforeMatchSpeedIncreases = 15f;

    private int numberOfActivePlayers;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        numberOfActivePlayers = 0;
        foreach (UnityGrid player in players)
        {            
            GameTimer gt = player.GetComponent<GameTimer>();
            if (gt != null)
            {
                numberOfActivePlayers++;
                gt.OnLose += Gt_OnLose;
            }
            player.OnCellsMatched += Player_OnCellsMatched;
        }     
    }

    private void Gt_OnLose(int userID)
    {
        foreach (UnityGrid player in players)
        {
            if (player.UserID == userID)
            {
                numberOfActivePlayers--;
                players.Remove(player);
                if (numberOfActivePlayers == 1)
                {
                    players[0].GetComponent<PlayerState>().OnPlayerWin();
                    playAgainCanvas.SetActive(true);
                    players[0].GetComponent<GameTimer>().enabled = false;
                }
                return;
            }
        }
    }

    private void Player_OnCellsMatched(int cellsMatched, int userId)
    {
        foreach(UnityGrid player in players)
        {
            GameTimer gt = player.GetComponent<GameTimer>();
            if (gt != null)
            {
                if (userId != player.UserID)
                {
                    gt.SubtractTime(cellsMatched);
                }
            }
        }   
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= secondsBeforeMatchSpeedIncreases)
        {
            currentTime -= secondsBeforeMatchSpeedIncreases;
            foreach (UnityGrid player in players)
            {
                player.DecrementMatchTime();
                UnityComputerOpponent computerPlayer = player.GetComponent<UnityComputerOpponent>();
                if (computerPlayer != null)
                {
                    computerPlayer.DecrementThinkTime();
                }
            }
        }
    }
}

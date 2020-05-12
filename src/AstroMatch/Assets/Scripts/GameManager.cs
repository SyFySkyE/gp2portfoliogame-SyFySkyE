using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playAgainCanvas;
    [Header("Game Parameters")]
    [SerializeField] private GameCountDown countdown;
    [Header("Number of moves before speed of matching and AI increases")]
    [SerializeField] private float secondsBeforeMatchSpeedIncreases = 15f;

    private List<UnityGrid> players;

    private int numberOfActivePlayers;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerList();
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

    private void InitializePlayerList()
    {
        players = new List<UnityGrid>();
        foreach(UnityGrid player in GetComponentsInChildren<UnityGrid>())
        {
            players.Add(player);
        }
    }

    private void Gt_OnLose(int userID)
    {
        foreach (UnityGrid player in players)
        {
            if (player.UserID == userID)
            {
                numberOfActivePlayers--;
                player.GetComponent<CommandProcessor>().DisablePlayer();
                players.Remove(player);
                if (numberOfActivePlayers == 1)
                {
                    players[0].GetComponent<PlayerState>().OnPlayerWin();
                    playAgainCanvas.SetActive(true);
                    SoundPlayer.Instance.PlayOneShot(SoundClips.End);
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
                player.GetComponent<GameTimer>().IncrementSubtractMultiplier();
                UnityComputerOpponent computerPlayer = player.GetComponent<UnityComputerOpponent>();
                if (computerPlayer != null)
                {
                    computerPlayer.DecrementThinkTime();
                }
            }
        }
    }
}

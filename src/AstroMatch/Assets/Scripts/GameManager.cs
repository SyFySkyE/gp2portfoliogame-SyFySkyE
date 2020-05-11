using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    class GameManager : MonoBehaviour
{
    [Header("Player parameters")]
    [SerializeField] private List<UnityGrid> players;
    [SerializeField] private GameCountDown countdown;

    private GameObject playAgainCanvas;
    private int numberOfActivePlayers;

    // Start is called before the first frame update
    void Start()
    {
        playAgainCanvas = Resources.Load<GameObject>("Prefabs/PlayAgainCanvas");
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
                    Instantiate(playAgainCanvas);
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
}

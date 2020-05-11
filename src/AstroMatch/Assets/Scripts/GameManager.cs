using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    class GameManager : MonoBehaviour
{
    [Header("Player parameters")]
    [SerializeField] private List<UnityGrid> players;
    [SerializeField] private GameCountDown countdown;

    // Start is called before the first frame update
    void Start()
    {
        foreach(UnityGrid player in players)
        {
            player.OnCellsMatched += Player_OnCellsMatched;
        }
    }

    private void Player_OnCellsMatched(int cellsMatched, int userId)
    {
        foreach(UnityGrid player in players)
        {
            GameTimer gt = player.GetComponent<GameTimer>();
            if (gt != null)
            {
                if (userId == player.UserID)
                {

                }
                else
                {
                    gt.SubtractTime(cellsMatched);
                }
            }
        }   
    }
}

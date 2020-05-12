using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameTimer))]
public class PlayerState : MonoBehaviour
{
    public event System.Action<int> OnPlayerLose;
    private GameObject winCanvas;
    private GameObject loseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas = Resources.Load<GameObject>("Prefabs/WinCanvas");
        loseCanvas = Resources.Load<GameObject>("Prefabs/LoseCanvas");
        GetComponent<GameTimer>().OnLose += PlayerState_OnLose;
    }

    private void PlayerState_OnLose(int userID)
    {
        OnPlayerLose?.Invoke(userID);
        Instantiate(loseCanvas, this.transform);
    }

    public void OnPlayerWin()
    {
        Instantiate(winCanvas, this.transform);
    }
}

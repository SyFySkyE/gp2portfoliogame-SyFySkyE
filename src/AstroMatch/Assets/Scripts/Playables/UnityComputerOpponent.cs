using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityComputerOpponent : UnityBasePlayer
{
    [Header("AI Opponent Parameter")]
    [SerializeField] private float secondsBeforeEnemyMove = 2f;
    [SerializeField] private float enemyTimeDecrement = 0.1f;

    private AI computerControlledOpponent;
    private float currentTime;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        computerControlledOpponent = new AI(GetComponent<UnityGrid>().ConceptualGrid);
        currentTime = 0f;
    }

    private void Update()
    {
        ComputerMoveTimer();
    }

    private void ComputerMoveTimer()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= secondsBeforeEnemyMove)
        {
            currentTime -= secondsBeforeEnemyMove;
            MakeAICommand();
        }
    }

    private void MakeAICommand()
    {
        SinglePiece connectedPiece = computerControlledOpponent.AICommand();
        SelectPiece selectPieceCommand = new SelectPiece(GetComponent<UnityGrid>().UnityPieces[(int)connectedPiece.Location.x, (int)connectedPiece.Location.y]);
        commandProcessor.AddNewCommand(selectPieceCommand);
    }
}

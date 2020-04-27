using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandProcessor : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private bool isComputerControlled;
    [SerializeField] private float secondsBeforeEnemyMove = 2f;
    [SerializeField] private float enemyTimeDecrement = 0.1f;

    private AI computerControlledOpponent;
    private byte maxCommandListSize = 10;
    private List<ICommand> commands;
    private float currentTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            SelectPiece newCommand = new SelectPiece(eventData.pointerEnter.GetComponent<UnityPiece>());
            AddNewCommand(newCommand);
        }
    }

    private void AddNewCommand(ICommand newCommand)
    {
        commands.Add(newCommand);
        if (commands.Count > maxCommandListSize)
        {
            commands.RemoveAt(0);
        }
        newCommand.Execute();
    }

    private void MakeAICommand()
    {
        
        Vector2 matchingDir;
        SinglePiece connectedPiece = computerControlledOpponent.SelectNextPiece(out matchingDir);        
        SelectPiece selectPieceCommand = new SelectPiece(GetComponent<UnityGrid>().UnityPieces[(int)connectedPiece.Location.x, (int)connectedPiece.Location.y]);
        AddNewCommand(selectPieceCommand);
        Debug.Log("dwdw");
        SelectPiece matchPieceCommand = new SelectPiece(GetComponent<UnityGrid>().UnityPieces[(int)connectedPiece.Location.x + (int)matchingDir.x, (int)connectedPiece.Location.y + (int)matchingDir.y]);
        AddNewCommand(matchPieceCommand);
    }

    // Start is called before the first frame update
    void Start()
    {
        commands = new List<ICommand>();
        if (isComputerControlled)
        {
            computerControlledOpponent = new AI(GetComponent<UnityGrid>().ConceptualGrid);
            currentTime = 0f;
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= secondsBeforeEnemyMove)
        {
            currentTime -= secondsBeforeEnemyMove;
            MakeAICommand();
        }
    }
}

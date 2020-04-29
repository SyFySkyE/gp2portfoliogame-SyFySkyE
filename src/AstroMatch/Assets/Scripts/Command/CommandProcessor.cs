using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandProcessor : MonoBehaviour
{
    [SerializeField] private bool isComputerControlled;
    
    private byte maxCommandListSize = 10;
    private List<ICommand> commands;    

    public void AddNewCommand(ICommand newCommand)
    {
        commands.Add(newCommand);
        if (commands.Count > maxCommandListSize)
        {
            commands.RemoveAt(0);
        }
        newCommand.Execute();
    }    

    // Start is called before the first frame update
    void Start()
    {
        commands = new List<ICommand>();
        if (isComputerControlled)
        {
            this.gameObject.AddComponent<UnityComputerOpponent>();
        }
        else
        {
            this.gameObject.AddComponent<PlayerController>();
        }
    }    
}

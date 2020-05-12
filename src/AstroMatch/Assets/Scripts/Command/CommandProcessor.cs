using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandProcessor : MonoBehaviour
{
    [Header("Check to enable AI")]
    [SerializeField] private bool isComputerControlled;
    
    private byte maxCommandListSize = 10;
    private List<ICommand> commands;
    private bool isEnabled;

    public void AddNewCommand(ICommand newCommand)
    {
        if (isEnabled)
        {
            commands.Add(newCommand);
            if (commands.Count > maxCommandListSize)
            {
                commands.RemoveAt(0);
            }
            newCommand.Execute();
        }        
    }    

    // Start is called before the first frame update
    void Start()
    {
        isEnabled = true;
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
    
    public void DisablePlayer()
    {
        isEnabled = false;
    }
}

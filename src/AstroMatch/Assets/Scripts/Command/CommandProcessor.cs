using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandProcessor : MonoBehaviour, IPointerDownHandler
{
    private byte maxCommandListSize = 10;
    private List<ICommand> commands;

    public void OnPointerDown(PointerEventData eventData)
    {
        SelectPiece newCommand = new SelectPiece(eventData.pointerEnter.GetComponent<UnityPiece>());
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
    }

    // Update is called once per frame
    void Update()
    {
        // Undo command logic
    }
}

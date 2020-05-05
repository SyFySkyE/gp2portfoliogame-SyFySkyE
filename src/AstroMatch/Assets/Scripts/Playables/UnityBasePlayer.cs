using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnityBasePlayer : MonoBehaviour, IPlayable
{
    public CommandProcessor commandProcessor { get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        commandProcessor = GetComponent<CommandProcessor>();
    }
}

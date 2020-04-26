using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ICommand
{
    public string CommandName;

    public Command()
    {
        this.CommandName = "Base Command";
    }

    public virtual void Execute()
    {

    }

    public virtual void Undo()
    {

    }
}

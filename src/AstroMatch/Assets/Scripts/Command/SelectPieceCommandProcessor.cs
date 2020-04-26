using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPieceCommandProcessor : MonoBehaviour
{
    private byte maxStackCount = 10;
    private Stack<ICommand> commands;    

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        this.gameObject.AddComponent<PieceMouseSelectCommand>();
# elif UNITY_STANDALONE
        this.gameObject.AddComponent<PieceMouseSelectCommand>();
#endif
        commands = new Stack<ICommand>();        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //UnityCommand newCommand = null;
            //newCommand = new PieceMouseSelectCommand();            
        }
    }
}

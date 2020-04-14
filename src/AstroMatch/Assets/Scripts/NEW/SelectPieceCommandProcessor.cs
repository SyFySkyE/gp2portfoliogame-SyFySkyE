using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPieceCommandProcessor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
#if UNITY_STANDALONE
        this.gameObject.AddComponent<PieceMouseSelectCommand>();
#endif
#endif
    }
}

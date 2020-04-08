using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCommandProcessor : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR
#if UNITY_STANDALONE
        this.gameObject.AddComponent<MouseSelectCommand>();
#endif
#endif
    }
}

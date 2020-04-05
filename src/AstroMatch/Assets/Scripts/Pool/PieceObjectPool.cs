﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceObjectPool : MonoBehaviour
{
    [Header("Pieces to Spawn")]
    [SerializeField] private Piece[] piecePrefabs;

    [Header("Object Pool Parameters")]
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxPoolSize = 20;

    // Start is called before the first frame update
    void Start()
    {
        CreateObjectPool();
    }

    private void CreateObjectPool()
    {
        PiecePool.Instance.CreatePool(piecePrefabs, initialPoolSize, maxPoolSize);
        PiecePool.Instance.PoolObject = this.gameObject;
    }
}

/*
 * Based off of David Crook's Object Pooling From https://blogs.msdn.microsoft.com/dave_crooks_dev_blog/2014/07/21/object-pooling-for-unity3d/
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePoolingManager 
{
    // Volitate to ensure that assignment must be finished before it is accessed
    private static volatile PiecePoolingManager instance;

    private Dictionary<string, PiecePool> objectPools;

    // Most light-weight object used for locking
    private static object syncRoot = new System.Object();

    private PiecePoolingManager()
    {
        this.objectPools = new Dictionary<string, PiecePool>();
    }

    public static PiecePoolingManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new PiecePoolingManager();
                    }
                }
            }
            return instance;
        }
    }

    public Piece PooledObject { get; internal set; }

    public bool CreatePool(Piece pieceToPool, int initialPoolSize, int maxPoolSize)
    {
        if (PiecePoolingManager.Instance.objectPools.ContainsKey(pieceToPool.name))
        {
            return false;
        }
        else
        {
            //PiecePool nPool = new PiecePool(initialPoolSize, maxPoolSize);
            //PiecePoolingManager.Instance.objectPools.Add(pieceToPool.name, nPool);
            return true;
        }
    }

    public Piece GetPiece(string pieceName)
    {
        return PiecePoolingManager.Instance.objectPools[pieceName].GetObject();
    }
}

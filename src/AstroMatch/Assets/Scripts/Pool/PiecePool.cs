﻿/*
 * Based off of David Crook's Object Pooling From https://blogs.msdn.microsoft.com/dave_crooks_dev_blog/2014/07/21/object-pooling-for-unity3d/
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePool 
{
    private static volatile PiecePool instance;
    private static object syncRoot = new System.Object();

    // Collection of stored Pieces.
    private List<Piece> pooledPieces;

    // Pool Size Parameters
    private int initialPoolSize;
    private int maxPoolSize;

    // Copies piece array in case we need to increase object pool
    private Piece[] pieceArray; // TODO Should we make it so we never need this? Ie, if we fill the pool with say, thirty pieces, will we ever need to refill the pool with new Instantiates?

    public GameObject PoolObject { get; internal set; }

    public static PiecePool Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new PiecePool();
                    }
                }
            }
            return instance;
        }
    }

    private PiecePool() { }

    public void CreatePool(Piece[] pieces, int initialPoolSize, int maxPoolSize)
    {
        pooledPieces = new List<Piece>();
        pieceArray = pieces;
        for (int i = 0; i < initialPoolSize; i++)
        {
            Debug.Log("Instantiated Piece");
            Piece newPiece = GameObject.Instantiate(pieces[Random.Range(0, pieces.Length)], Vector3.zero, Quaternion.identity);

            newPiece.gameObject.SetActive(true);
            pooledPieces.Add(newPiece);

            GameObject.DontDestroyOnLoad(newPiece);
        }

        this.initialPoolSize = initialPoolSize;
        this.maxPoolSize = maxPoolSize;        
    }

    public Piece GetObject()
    {
        for (int i = 0; i < pooledPieces.Count; i++)
        {
            if (pooledPieces[i].gameObject.activeSelf == false)
            {
                pooledPieces[i].gameObject.SetActive(true);
                return pooledPieces[i];
            }
            else
            {
                // If we didn't make it this far, there isn't an inactive object in the pool.
                if (this.maxPoolSize > this.pooledPieces.Count)
                {
                    Debug.Log("Instantiated Piece, pool too small");
                    Piece newPiece = GameObject.Instantiate(pieceArray[Random.Range(0, pieceArray.Length)], Vector3.zero, Quaternion.identity);
                    newPiece.gameObject.SetActive(true);
                    pooledPieces.Add(newPiece);
                    return newPiece;
                }
            }
        }
        Debug.LogError("Cannot retrieve piece from pool");
        return null;
    }

    public void AddPieceBackToPool(Piece currentPiece)
    {
        currentPiece = pieceArray[Random.Range(0, pieceArray.Length)]; // Randomize piece type
        currentPiece.gameObject.SetActive(false); // This fucks up the prefab for some reason?
        if (this.maxPoolSize > this.pooledPieces.Count)
        {
            pooledPieces.Add(currentPiece);
        }
        else
        {
            Debug.LogError("Tried adding piece back to pool but pool is full! Destroying object...");
            GameObject.Destroy(currentPiece);
        }
    }
}
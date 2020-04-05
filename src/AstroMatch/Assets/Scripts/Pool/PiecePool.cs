/*
 * Based off of David Crook's Object Pooling From https://blogs.msdn.microsoft.com/dave_crooks_dev_blog/2014/07/21/object-pooling-for-unity3d/
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePool 
{
    // Collection of stored Pieces.
    private List<Piece> pooledPieces;

    // Collection of game pieces
    private Piece[] samplePieceArray;

    // Samples of pieces
    private IcePiece icePiece;
    private WaterPiece waterPiece;
    private SandPiece sandPiece;
    private PurplePiece purplePiece;
    private RedPiece redPiece;

    // Pool Size Parameters
    private int initialPoolSize;
    private int maxPoolSize;

    public PiecePool(int initialPoolSize, int maxPoolSize)
    {
        samplePieceArray = GetPieceArray();
        pooledPieces = new List<Piece>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            Debug.Log("Instantiated Piece");
            Piece newPiece = GameObject.Instantiate(samplePieceArray[Random.Range(0, samplePieceArray.Length)], Vector3.zero, Quaternion.identity);

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
                    Piece newPiece = GameObject.Instantiate(samplePieceArray[Random.Range(0, samplePieceArray.Length)], Vector3.zero, Quaternion.identity);
                    newPiece.gameObject.SetActive(true);
                    pooledPieces.Add(newPiece);
                    return newPiece;
                }
            }
        }
        Debug.LogError("Cannot retrieve object from pool");
        return null;
    }

    private Piece[] GetPieceArray()
    {
        Piece[] samplePieces = new Piece[4];
        samplePieces[0] = icePiece;
        samplePieces[1] = waterPiece;
        samplePieces[2] = sandPiece;
        samplePieces[3] = purplePiece;
        samplePieces[4] = redPiece;
        return samplePieces;
    }
}

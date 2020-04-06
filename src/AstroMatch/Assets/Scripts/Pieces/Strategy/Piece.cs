using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class Piece : MonoBehaviour, IMatachable, ILoggable
{
    public Sprite PieceImage { get => gamePieceImage.sprite; }
    protected bool isNull = false;
    protected Image gamePieceImage;

    public PieceType PieceCurrentType
    {
        get { return this.pieceType; }
        set
        {
            this.initialPieceType = this.pieceType;
            this.pieceType = value;
        }
    }
    public PieceType InitialPieceType { get => this.initialPieceType; }
    
    protected PieceType initialPieceType;
    public PieceState PieceCurrentState
    {
        get
        {
            return this.currentState;
        }
        set
        {
            this.initialPieceState = this.currentState;
            this.currentState = value;
        }
    }
    public PieceState InitialPieceState { get => this.initialPieceState; }
    protected PieceState initialPieceState;
    protected PieceType pieceType;
    protected PieceState currentState;
    public RectTransform PieceRectTransform
    {
        get
        {
            if (this.pieceRectTransform == null) // At first initialization, this gets called too fast
            {
                this.pieceRectTransform = GetComponent<RectTransform>();
            }

            return this.pieceRectTransform;
        }
    } // TODO These we probably don't need

    protected RectTransform pieceRectTransform;    

    public virtual void AddToPool() // Overwritten by subclass to use pools
    {
        Destroy(this); 
        Debug.LogError(this.name + " was destroyed!");
    }

    public virtual void SwitchPlace(Vector2 dir)
    {
        Debug.Log("Switched places toward " + dir);
    }

    public virtual void ResetTransform()
    {
        this.PieceRectTransform.localPosition = Vector3.zero;
    }

    public string Log() 
    {
       return ($"PieceType: {this.pieceType} PieceState: {this.PieceCurrentState}");
    }

    public virtual void SetupPiece()
    {
        // Subclasses should overwrite this
    }
}

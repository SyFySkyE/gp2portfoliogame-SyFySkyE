using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public abstract class Piece : MonoBehaviour, IMatachable, IPointerClickHandler // TODO Input info should be in separate class, and easily extendedable for touch and other systems
{
    [Header("Piece Parameters")]
    [SerializeField] private Sprite pieceImage;
    public Sprite PieceImage { get => gamePieceImage.sprite; }
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
    public Vector2 CellLocation
    {
        get
        {
            return this.cellLoc;
        }
        set
        {
            this.initialCellLocation = this.cellLoc;
            this.cellLoc = value;            
        }
    }

    public Vector2 InitialCellLocation { get => this.initialCellLocation; }
    protected Vector2 initialCellLocation;

    protected Vector2 cellLoc;    
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
    }

    private RectTransform pieceRectTransform;
    public static event System.Action<Piece> OnSelectThisPiece; // TODO Switch to proper observer

    public virtual void Match()
    {
        Debug.Log("Match");
    }

    public virtual void SwitchPlace(Vector2 dir)
    {
        Debug.Log("Switched places toward " + dir);
    }

    // Start is called before the first frame update
    protected virtual void Start() // Needs to be protected so it is called by its derived classes
    {
        pieceRectTransform = GetComponent<RectTransform>();
        gamePieceImage = GetComponent<Image>();
        gamePieceImage.sprite = pieceImage;
        initialCellLocation = cellLoc;
        initialPieceState = currentState;
        initialPieceType = pieceType;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectObject();
    }

    private void SelectObject()
    {
        OnSelectThisPiece?.Invoke(this);
    }

    public string Log()
    {
       return ($"PieceType: {this.pieceType} PieceState: {this.PieceCurrentState} at grid location: {this.CellLocation}");
    }
}

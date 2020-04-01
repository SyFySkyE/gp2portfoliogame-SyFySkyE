using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler, ILoggable
{
    public Vector2 CellLocation;    
    private SpriteRenderer spriteRender;
    public Piece PieceInCell
    {
        get
        {
            return this.pieceInCell;
        }
        set
        {
            this.initialPiece = this.pieceInCell;
            this.pieceInCell = value;
        }
    }
    private Piece pieceInCell;
    public Piece InitialPiece { get => this.initialPiece; }
    private Piece initialPiece;
    public RectTransform RectTransform 
    { 
        get
        {
            if (this.rectTransform == null)
            {
                this.rectTransform = GetComponent<RectTransform>();
            }

            return this.rectTransform;
        } 
    }
    private RectTransform rectTransform;
    public static event System.Action<Cell> OnSelectThisCell; // TODO Switch to proper observer

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData) // TODO Switch to CommandPattern!!
    {
        SelectCell();
    }

    private void SelectCell()
    {
        OnSelectThisCell?.Invoke(this);
    }

    public string Log()
    {
        return $"{this.name} is located at: {this.CellLocation}, contains piece {this.PieceInCell}";
    }
}

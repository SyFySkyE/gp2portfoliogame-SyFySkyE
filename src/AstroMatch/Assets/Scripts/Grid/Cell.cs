using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler, ILoggable
{
    public Vector2 CellLocation;
    private UnityEngine.UI.Image spriteImage;
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
        spriteImage = GetComponent<UnityEngine.UI.Image>();
    }

    public void OnPointerClick(PointerEventData eventData) // TODO Switch to CommandPattern!!
    {
        if (this.GetType() != typeof(NullPiece))
        {            
            SelectCell();
        }        
    }

    private void SelectCell()
    {
        spriteImage.enabled = !spriteImage.enabled;
        OnSelectThisCell?.Invoke(this);
    }

    public void DeSelectCell()
    {
        spriteImage.enabled = false;
    }

    public void SetupPieceTransform()
    {
        this.PieceInCell.gameObject.SetActive(true);
        this.PieceInCell.transform.SetParent(this.transform);
        this.PieceInCell.ResetTransform();
    }

    public string Log()
    {
        return $"{this.name} is located at: {this.CellLocation}, contains piece {this.PieceInCell}";
    }

    public void TakePieceOut() 
    {
        if (this.PieceInCell != null)
        {            
            PieceInCell.AddToPool();
            this.PieceInCell = null;
        }        
    }
}

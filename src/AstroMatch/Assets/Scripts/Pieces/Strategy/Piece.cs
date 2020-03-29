using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class Piece : MonoBehaviour, IMatachable
{
    [Header("Piece Parameters")]
    [SerializeField] private Sprite pieceImage;
    public Sprite PieceImage { get => gamePieceImage.sprite; }
    protected Image gamePieceImage;

    public PieceType PieceCurrentType { get => this.pieceType; }
    public PieceState PieceCurrentState { get => this.currentState; }
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
    }
}

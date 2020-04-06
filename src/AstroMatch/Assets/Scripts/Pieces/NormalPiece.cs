using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPiece : Piece
{
    [Header("For DEBUG Purposes!!")]
    [SerializeField] private PieceType typeToChange;
    [SerializeField] private PieceState stateToChange;

    public override void SetupPiece()
    {
        this.PieceCurrentType = (PieceType)Random.Range(1, System.Enum.GetValues(typeof(PieceType)).Length); // Get Random Type
        LazyLoadComponents();
        SetupImage();
        this.ResetTransform();
    }

    private void SetupDebugPiece()
    {
        this.PieceCurrentType = this.typeToChange;
        LazyLoadComponents();
        this.ResetTransform();
        SetupImage();
    }

    private void LazyLoadComponents()
    {
        if (pieceRectTransform == null) pieceRectTransform = GetComponent<RectTransform>();
        if (gamePieceImage == null) gamePieceImage = GetComponent<UnityEngine.UI.Image>();
    }

    private void SetupImage()
    {
        this.gamePieceImage.sprite = Resources.Load<Sprite>($"Art/Sprites/Pieces/{this.PieceCurrentType.ToString()}");
    }
#if UNITY_EDITOR
    private void Start()
    {
        this.typeToChange = this.PieceCurrentType;
    }

    private void Update()
    {
        if (this.PieceCurrentType != typeToChange)
        {
            SetupDebugPiece();
        }
    }
#endif

    public override void AddToPool() // TODO Can be ambigious between this match and Cell's Match(). Also doesn't really explain what it does exactly
    {
        PiecePool.Instance.AddPieceBackToPool(this);        
    }
}
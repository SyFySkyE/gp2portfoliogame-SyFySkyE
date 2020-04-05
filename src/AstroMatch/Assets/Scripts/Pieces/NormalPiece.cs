﻿using System.Collections;
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
    }

    private void SetupDebugPiece()
    {
        this.PieceCurrentType = this.typeToChange;
        LazyLoadComponents();
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
}
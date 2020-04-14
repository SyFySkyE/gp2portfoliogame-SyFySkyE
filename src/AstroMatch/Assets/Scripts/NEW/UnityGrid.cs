using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityGrid : MonoBehaviour
{
    [Header("Grid Parameters")]
    [SerializeField] private int numberOfColumns = 8;
    [SerializeField] private int numberOfRows = 8;

    [Header("Unity Piece Prefab")]
    [SerializeField] private UnityPiece unityPiece;

    [Header("TODO Load this from resources")]
    [SerializeField] private Sprite waterImage;
    [SerializeField] private Sprite iceImage;
    [SerializeField] private Sprite redImage;
    [SerializeField] private Sprite purpleImage;
    [SerializeField] private Sprite sandImage;
    [SerializeField] private Sprite emptyImage;

    private Grid conceptualGrid; // Conceptual Grid, pure logic
    private UnityPiece[,] unityPieces; // Visual Concrete GO Array
    private UnityEngine.UI.Image playField; // TODO Should this be exposed? Also this visually breaks when Rows and/or columns get changed!    

    private SinglePiece pieceSelected;

    // Start is called before the first frame update
    void Start()
    {
        UnityPiece.OnPieceSelect += UnityPiece_OnPieceSelect;
        conceptualGrid = new Grid(numberOfColumns, numberOfRows);
        unityPieces = new UnityPiece[numberOfColumns + 2, numberOfRows + 2];
        playField = GetComponent<Image>();
        DrawCells();
    }

    private void UnityPiece_OnPieceSelect(Vector2 location)
    {
        Debug.Log("Dwdw");
        if (conceptualGrid.PieceArray[(int)location.x, (int)location.y].PieceType != SinglePieceType.None)
        {
            SelectPiece(location);            
        }
    }

    private void SelectPiece(Vector2 location)
    {       
        if (pieceSelected == null)
        {
            Debug.Log("dwdw");
            pieceSelected = conceptualGrid.PieceArray[(int)location.x, (int)location.y];
            unityPieces[(int)location.x, (int)location.y].SelectPiece();
        }
        else if (pieceSelected.Location == location)
        {
            Debug.Log("Dwdw");
            pieceSelected = null;
            unityPieces[(int)location.x, (int)location.y].DeselectPiece();
        }
        else
        {
            AttemptToSwap(location);
        }        
    }

    private void AttemptToSwap(Vector2 newLocation)
    {
        foreach (Vector2 dir in Directions.AllDirections)
        {
            if ((this.pieceSelected.Location) + dir == conceptualGrid.PieceArray[(int)newLocation.x, (int)newLocation.y].Location)
            {
                pieceSelected = null;
                Debug.Log("Clicked a good peice");
                return;
            }
        }
        pieceSelected = null;
    }

    private void DrawCells()
    {
        RectTransform unityPieceRect = unityPiece.GetComponent<RectTransform>();
        float pieceSizeWidth = unityPieceRect.rect.width;
        float pieceSizeHeight = unityPieceRect.rect.height;

        float xPiecePlacement = -(playField.rectTransform.rect.width / 2) - (pieceSizeWidth / 2);
        float yPiecePlacement = (playField.rectTransform.rect.height / 2) + (pieceSizeHeight / 2);
        Vector2 piecePlacement = new Vector2(xPiecePlacement, yPiecePlacement); // Where the pieces should start spawning

        foreach (SinglePiece currentPiece in conceptualGrid.PieceArray)
        {
            UnityPiece newPiece = Instantiate(unityPiece, this.transform, false);
            newPiece.InitializeLocation(currentPiece.Location);
            newPiece.SetImage(GetSprite(currentPiece));
            newPiece.SetTransform(piecePlacement);
            piecePlacement.x += pieceSizeWidth;
            if (piecePlacement.x > playField.rectTransform.rect.width / 2 + (pieceSizeWidth / 2))
            {
                piecePlacement.y -= pieceSizeHeight;
                piecePlacement.x = -(playField.rectTransform.rect.width / 2) - (pieceSizeWidth / 2);
            }
            unityPieces[(int)currentPiece.Location.x, (int)currentPiece.Location.y] = newPiece;
        }
    }

    private Sprite GetSprite(SinglePiece piece)
    {
        switch (piece.PieceType)
        {
            case SinglePieceType.Ice:
                return iceImage;
            case SinglePieceType.Purple:
                return purpleImage;
            case SinglePieceType.Red:
                return redImage;
            case SinglePieceType.Sand:
                return sandImage;
            case SinglePieceType.Water:
                return waterImage;
            default:
                return emptyImage;
        }
    }
}

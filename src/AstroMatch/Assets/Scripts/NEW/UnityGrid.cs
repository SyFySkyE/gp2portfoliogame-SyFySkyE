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
        conceptualGrid = new Grid(numberOfColumns, numberOfRows);
        unityPieces = new UnityPiece[numberOfColumns + 2, numberOfRows + 2];        
        playField = GetComponent<Image>();
        DrawCells();
        foreach (UnityPiece uPiece in unityPieces) 
        {
            uPiece.OnPieceSelect += UnityPiece_OnPieceSelect;
        }
    }

    private void UnityPiece_OnPieceSelect(Vector2 location)
    {
        if (conceptualGrid.PieceArray[(int)location.x, (int)location.y].PieceType != SinglePieceType.None) // We're clicking a cell that's not a null piece
        {
            SelectPiece(location);
        }
    }

    private void SelectPiece(Vector2 location)
    {
        if (pieceSelected == null)
        {
            unityPieces[(int)location.x, (int)location.y].UISelectPiece();
            pieceSelected = conceptualGrid.PieceArray[(int)location.x, (int)location.y];
        }
        else if (pieceSelected.Location == location) // If we click the same space
        {
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
        unityPieces[(int)pieceSelected.Location.x, (int)pieceSelected.Location.y].DeselectPiece();
        unityPieces[(int)newLocation.x, (int)newLocation.y].DeselectPiece();

        foreach (Vector2 dir in Directions.AllDirections)
        {
            if ((this.pieceSelected.Location) + dir == conceptualGrid.PieceArray[(int)newLocation.x, (int)newLocation.y].Location)
            {
                SwapPieces(pieceSelected.Location, newLocation);
                pieceSelected = null;
                return;
            }
        }
        pieceSelected = null;
    }

    private void SwapPieces(Vector2 pieceOneLoc, Vector2 pieceTwoLoc)
    {
        UpdateConceptualGrid(pieceOneLoc, pieceTwoLoc); // Update conceptual array and check for match
        if (Matching.CheckForMatch(conceptualGrid.PieceArray[(int)pieceOneLoc.x, (int)pieceOneLoc.y], conceptualGrid.PieceArray, Directions.AllDirections) ||
            Matching.CheckForMatch(conceptualGrid.PieceArray[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y], conceptualGrid.PieceArray, Directions.AllDirections))
        {
            UpdateGameObjectGrid(pieceOneLoc, pieceTwoLoc); // If there is a match, update the GO array as well.      
            if (Matching.CheckForMatch(conceptualGrid.PieceArray[(int)pieceOneLoc.x, (int)pieceOneLoc.y], conceptualGrid.PieceArray, Directions.AllDirections))
            {
                Match(pieceOneLoc);
            }
            if (Matching.CheckForMatch(conceptualGrid.PieceArray[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y], conceptualGrid.PieceArray, Directions.AllDirections))
            {
                Match(pieceTwoLoc);
            }
        }
        else
        {
            UpdateConceptualGrid(pieceTwoLoc, pieceOneLoc); // If there isn't a match, switch it back to where it was. No point in updating GO array.
        }
    }

    private void Match(Vector2 pieceLoc)
    {        
        foreach (SinglePiece matchingPiece in Matching.GetConnectedPieces(conceptualGrid.PieceArray[(int)pieceLoc.x, (int)pieceLoc.y], conceptualGrid.PieceArray, Directions.AllDirections))
        {
            Debug.Log(matchingPiece.PieceType);
            Debug.Log(matchingPiece.Location);
            conceptualGrid.SetPieceToNull(matchingPiece.Location);
            unityPieces[(int)matchingPiece.Location.x, (int)matchingPiece.Location.y].SetImage(GetSprite(matchingPiece));
            
        }
    }

    private void UpdateGameObjectGrid(Vector2 pieceOneLoc, Vector2 pieceTwoLoc)
    {
        Vector2 pieceOneCachedLoc = unityPieces[(int)pieceOneLoc.x, (int)pieceOneLoc.y].pieceLocation;
        unityPieces[(int)pieceOneLoc.x, (int)pieceOneLoc.y].InitializeLocation(unityPieces[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y].pieceLocation);
        unityPieces[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y].InitializeLocation(pieceOneCachedLoc);

        Vector3 pieceOneRectLoc = unityPieces[(int)pieceOneLoc.x, (int)pieceOneLoc.y].UnityPieceRectTransform.localPosition;
        unityPieces[(int)pieceOneLoc.x, (int)pieceOneLoc.y].SetTransform(unityPieces[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y].UnityPieceRectTransform.localPosition);
        unityPieces[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y].SetTransform(pieceOneRectLoc);

        UnityPiece unityPieceOne = unityPieces[(int)pieceOneLoc.x, (int)pieceOneLoc.y];
        unityPieces[(int)pieceOneLoc.x, (int)pieceOneLoc.y] = unityPieces[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y];
        unityPieces[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y] = unityPieceOne;
    }

    private void UpdateConceptualGrid(Vector2 pieceOneLoc, Vector2 pieceTwoLoc)
    {
        SinglePiece pieceOne = conceptualGrid.PieceArray[(int)pieceOneLoc.x, (int)pieceOneLoc.y];
        conceptualGrid.PieceArray[(int)pieceOneLoc.x, (int)pieceOneLoc.y] = conceptualGrid.PieceArray[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y];
        conceptualGrid.PieceArray[(int)pieceTwoLoc.x, (int)pieceTwoLoc.y] = pieceOne;
        conceptualGrid.ResetPieceLocation(pieceOneLoc);
        conceptualGrid.ResetPieceLocation(pieceTwoLoc);
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
#if UNITY_EDITOR
    private void Update()
    {
        DebugPieceType();
    }

    private void DebugPieceType() // Press corresponding key to change type. For testing edge cases and specific scenarios
    {
        if (pieceSelected != null && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                pieceSelected.PieceType = SinglePieceType.Ice;               
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                pieceSelected.PieceType = SinglePieceType.Red;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                pieceSelected.PieceType = SinglePieceType.Water;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                pieceSelected.PieceType = SinglePieceType.Purple;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                pieceSelected.PieceType = SinglePieceType.Sand;
            }            
            conceptualGrid.SetPieceType(pieceSelected.Location, pieceSelected.PieceType);
            unityPieces[(int)pieceSelected.Location.x, (int)pieceSelected.Location.y].SetImage(GetSprite(pieceSelected));
        }
    }
#endif
}

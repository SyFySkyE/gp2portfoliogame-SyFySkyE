using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("This player's grid")]
    [SerializeField] private PlayGrid playerGrid;

    private Piece pieceSelected;    

    #region EventSubscriptions
    private void OnEnable()
    {
        Piece.OnSelectThisPiece += Piece_OnSelectThisPiece;        
    }
    private void OnDisable()
    {
        Piece.OnSelectThisPiece -= Piece_OnSelectThisPiece;
    }
    #endregion

    private void Piece_OnSelectThisPiece(Piece clickedPiece)
    {
        if (pieceSelected == null)
        {
            SelectPiece(clickedPiece);
        }
        else
        {
            AttemptToSwap(clickedPiece);
        }        
    }

    private void SelectPiece(Piece clickedPiece)
    {
        pieceSelected = clickedPiece;
        this.pieceSelected.Log();
    }

    private void AttemptToSwap(Piece clickedPiece)
    {
        foreach (Vector2 direction in Directions.directions)
        {
            if ((this.pieceSelected.CellLocation) + direction == clickedPiece.CellLocation)
            {
                Debug.Log($"Can Swap. Piece Selected: {pieceSelected.Log()} with Piece Clicked: {clickedPiece.Log()}");
                playerGrid.SwapPieces(pieceSelected, clickedPiece);
                pieceSelected = null;
                return;
            }
        }
        Debug.Log($"Can NOT Swap. Piece Selected: {pieceSelected.Log()} with Piece Clicked: {clickedPiece.Log()}"); // If it reaches this far, a match was not found.
        pieceSelected = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!playerGrid) throw new System.Exception($"{this.name} player does NOT have a play grid assigned! Please assign it in the inspector. - CG");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

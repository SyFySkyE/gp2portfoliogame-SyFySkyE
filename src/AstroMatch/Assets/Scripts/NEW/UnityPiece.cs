using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPiece : MonoBehaviour
{
    private UnityEngine.UI.Image spriteImage;
    public Vector2 pieceLocation;
    public static event System.Action<Vector2> OnPieceSelect;

    private bool isSelected = false;

    private void Start()
    {
        spriteImage = GetComponent<UnityEngine.UI.Image>();
    }

    public void InitializeLocation(Vector2 newLoc)
    {
        pieceLocation = newLoc;
    }

    public void SelectPiece()
    {
        isSelected = !isSelected;
        OnPieceSelect?.Invoke(this.pieceLocation);
    }

    private void Update()
    {
        //if (isSelected)
        //{
        //    spriteImage.color = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(Time.time, 1));
        //}
        //else
        //{
        //    spriteImage.color = Color.white;
        //}
    }
}

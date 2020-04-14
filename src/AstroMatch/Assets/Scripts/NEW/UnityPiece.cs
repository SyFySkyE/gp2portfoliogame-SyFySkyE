using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPiece : MonoBehaviour
{
    public UnityEngine.UI.Image SpriteImage
    {
        get
        {
            if (this.spriteImage == null)
            {
                this.spriteImage = GetComponent<UnityEngine.UI.Image>();
            }
            return this.spriteImage;
        }
    }
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
        isSelected = true;
        OnPieceSelect?.Invoke(this.pieceLocation);
    }

    public void DeselectPiece()
    {
        isSelected = false;
    }

    public void SetImage(Sprite sprite)
    {
        this.SpriteImage.sprite = sprite;
    }

    public void SetTransform(Vector2 transform)
    {
        GetComponent<RectTransform>().localPosition = transform;
    }

    private void Update()
    {
        if (isSelected)
        {
            spriteImage.color = Color.Lerp(Color.white, Color.clear, Mathf.PingPong(Time.time, 1));
        }
        else
        {
            spriteImage.color = Color.white;
        }
    }
}

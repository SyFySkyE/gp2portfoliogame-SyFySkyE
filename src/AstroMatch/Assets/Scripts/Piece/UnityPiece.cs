using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityPiece : MonoBehaviour
{
    [Header("How fast the sprite fades out upon matching")]
    [SerializeField] private float fadeMultiplier = 0.2f;
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
    public event System.Action<Vector2> OnPieceSelect;
    public RectTransform UnityPieceRectTransform
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

    private bool isSelected = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        spriteImage = GetComponent<UnityEngine.UI.Image>();
    }

    public void InitializeLocation(Vector2 newLoc)
    {
        pieceLocation = newLoc;
    }

    public void SelectPiece()
    {
        OnPieceSelect?.Invoke(this.pieceLocation);
    }

    public void UISelectPiece()
    {
        isSelected = true;
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
        this.UnityPieceRectTransform.localPosition = transform;
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

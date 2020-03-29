using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Image))]
public abstract class Piece : MonoBehaviour, IMatachable
{
    [Header("Piece Parameters")]
    [SerializeField] private Sprite pieceImage;
    public Sprite PieceImage { get => gamePieceImage.sprite; }
    private Image gamePieceImage;

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

    private AudioSource pieceAudioSource;

    [SerializeField] private AudioClip switchPlacesSfx;
    [SerializeField] private float switchPlacesVolume = 0.5f;

    [SerializeField] private AudioClip matchSfx;
    [SerializeField] private float matchVolume = 0.5f;    

    public virtual void Match()
    {
        pieceAudioSource.PlayOneShot(matchSfx, matchVolume);
    }

    public virtual void SwitchPlace(Vector2 dir)
    {
        pieceAudioSource.PlayOneShot(switchPlacesSfx, switchPlacesVolume);
    }

    // Start is called before the first frame update
    private void Start()
    {
        pieceAudioSource = GetComponent<AudioSource>();
        pieceRectTransform = GetComponent<RectTransform>();
        gamePieceImage = GetComponent<Image>();
        gamePieceImage.sprite = pieceImage;
    }
}

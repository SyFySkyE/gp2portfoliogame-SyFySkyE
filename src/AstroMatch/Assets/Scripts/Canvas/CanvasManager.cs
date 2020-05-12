using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreenCanvas;
    [SerializeField] private GameObject creditsCanvas;
    [SerializeField] private GameObject instructionsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        EnableTitleCanvas();
    }

    public void EnableTitleCanvas()
    {
        titleScreenCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
        instructionsCanvas.SetActive(false);
    }

    public void EnableCreditsCanvas()
    {
        titleScreenCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        instructionsCanvas.SetActive(false);
    }

    public void EnableIntructionCanvas()
    {
        titleScreenCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
        instructionsCanvas.SetActive(true);
    }
}

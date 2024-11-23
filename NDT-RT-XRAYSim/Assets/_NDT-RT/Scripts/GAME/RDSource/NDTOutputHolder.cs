using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDTOutputHolder : MonoBehaviour
{
    public RawImage outputImage;
    public Canvas canvas;
    public CanvasGroup filmLayer;
    //[SerializeField] private bool generated = false;
    [SerializeField] private float filmGenerationTime = 2f;

    public void RemoveLayer(float percent)
    {
        Debug.LogWarning("Generating...");
        //if (generated) return;

        outputImage.DOFade(percent, filmGenerationTime).OnComplete(() =>
        {
            //generated = true;
        });

        float currentFilmPercent = filmLayer.alpha;

        filmLayer.DOFade((currentFilmPercent - percent), filmGenerationTime);
    }
}
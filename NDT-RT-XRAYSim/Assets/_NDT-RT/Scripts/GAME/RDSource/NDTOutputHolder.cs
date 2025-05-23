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

    public bool canBeStickedOn = false;

    public void RemoveLayer(float percent, float generationTime)
    {
        Debug.LogWarning("Generating...");
        //if (generated) return;

        outputImage.DOFade(percent, generationTime).OnComplete(() =>
        {
            //generated = true;
            if(outputImage.color.a == 1)
            {
                canBeStickedOn = true;
            }
        });

        float currentFilmPercent = filmLayer.alpha;

        filmLayer.DOFade((currentFilmPercent - percent), generationTime);
    }
}
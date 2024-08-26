using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Item
{
    [SerializeField] private GameObject _itemCanvas;
    public bool isItem = false;

    public bool isInteracted = false;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isInteracted = rb.isKinematic;
    }


    public void ShowTooltip()
    {
        if (!isItem) return;

        _itemCanvas.SetActive(true);
        _itemCanvas.GetComponent<ItemContainerUI>().SetItemInfo(ItemName);
    }

    public void HideTooltip()
    {
        if(!isItem) return; 

        _itemCanvas.SetActive(false);
    }
}
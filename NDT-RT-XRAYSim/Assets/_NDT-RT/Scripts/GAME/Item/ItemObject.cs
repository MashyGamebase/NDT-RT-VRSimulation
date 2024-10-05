using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemObject : Item
{
    [SerializeField] private GameObject _itemCanvas;
    public bool isItem = false;

    [SerializeField] private XRGrabInteractable _XRGrab;
    [SerializeField] private GameObject defect;
    [SerializeField] internal bool hasDefect;

    public bool isInteracted = false;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _XRGrab = GetComponent<XRGrabInteractable>();

        hasDefect = ChanceToHaveDefect(0.35f); //35% chance to have a defect
        defect.SetActive(hasDefect);
    }

    private void Update()
    {
        if (_XRGrab.isSelected)
        {
            rb.isKinematic = true;
            ShowTooltip();
        }
        else if(!_XRGrab.isSelected)
        {
            rb.isKinematic = false;
            HideTooltip();
        }
    }

    public bool ChanceToHaveDefect(float chance)
    {
        if (Random.value <= chance)
        {
            return true;
        }
        else
        {
            return false;
        }
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
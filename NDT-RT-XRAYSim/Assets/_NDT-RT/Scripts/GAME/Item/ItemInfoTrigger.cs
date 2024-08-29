using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ItemInfoTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemFlaw;
    [SerializeField] private TextMeshProUGUI _itemSize;
    [SerializeField] private TextMeshProUGUI _itemWeight;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Item>())
        {
            Item otherObject = other.gameObject.GetComponent<Item>();
            SetItemInformation(otherObject.ItemName, otherObject.ItemFlaw, otherObject.ItemSize, otherObject.ItemWeight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Item>())
        {
            RemoveItemInformation();
        }
    }

    private void SetItemInformation(string itemName, string itemFlaw, float itemSize, float itemWeight)
    {
        _itemName.SetText(itemName);
        _itemFlaw.SetText(itemFlaw);
        _itemSize.SetText(itemSize.ToString() + " in");
        _itemWeight.SetText(itemWeight.ToString() + " kg");
    }

    private void RemoveItemInformation()
    {
        _itemName.SetText("");
        _itemFlaw.SetText("");
        _itemSize.SetText("");
        _itemWeight.SetText("");
    }
}
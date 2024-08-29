using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private string _itemName;
    [SerializeField] private float _itemWeight;
    [SerializeField] private float _itemSize;
    [SerializeField] private string _itemFlaw;

    public string ItemName => _itemName;
    public float ItemWeight => _itemWeight;
    public float ItemSize => _itemSize;
    public string ItemFlaw => _itemFlaw;
}
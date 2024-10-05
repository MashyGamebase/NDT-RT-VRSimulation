using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] internal string _itemName;
    [SerializeField] internal float _itemWeight;
    [SerializeField] internal float _itemSize;
    [SerializeField] internal string _itemFlaw;

    public string ItemName => _itemName;
    public float ItemWeight => _itemWeight;
    public float ItemSize => _itemSize;
    public string ItemFlaw => _itemFlaw;
}
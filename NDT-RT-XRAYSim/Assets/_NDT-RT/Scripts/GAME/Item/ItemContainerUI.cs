using TMPro;
using UnityEngine;

public class ItemContainerUI : MonoBehaviour
{
    public TextMeshProUGUI _itemName, _itemWeight, _itemSize;

    [SerializeField] private Transform playerCameraTransform;

    private void Start()
    {
        playerCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Apply the billboard effect to make the UI element always face the camera
        if (playerCameraTransform != null)
        {
            transform.LookAt(transform.position + playerCameraTransform.rotation * Vector3.forward,
                             playerCameraTransform.rotation * Vector3.up);
        }
    }

    public void SetItemInfo(string itemName = "DefaultItemName", float itemSize = 0, float itemWeight = 0)
    {
        _itemName?.SetText(itemName);
        _itemWeight?.SetText(itemWeight.ToString());
        _itemSize?.SetText(itemSize.ToString());
    }
}
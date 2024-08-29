using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFPSLook : MonoBehaviour
{
    [SerializeField] float sensitivityX = 8f;
    [SerializeField] float sensitivityY = 0.5f;
    float mouseX, mouseY;

    [SerializeField] Transform playerCamera;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;

    [SerializeField] private PlayerObjectInteraction playerObjectInteraction; // Reference to PlayerObjectInteraction script

    private void Update()
    {
        // Check if the player is rotating an object
        if (playerObjectInteraction == null || !playerObjectInteraction.IsRotatingObject())
        {
            // Rotate player horizontally
            transform.Rotate(Vector3.up * mouseX);

            // Rotate camera vertically
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;
            playerCamera.eulerAngles = targetRotation;
        }
    }

    #region CALLBACK_CONTEXT
    public void OnMouseLookX(InputAction.CallbackContext context)
    {
        mouseX = context.ReadValue<float>() * sensitivityX;
    }

    public void OnMouseLookY(InputAction.CallbackContext context)
    {
        mouseY = context.ReadValue<float>() * sensitivityY;
    }
    #endregion
}

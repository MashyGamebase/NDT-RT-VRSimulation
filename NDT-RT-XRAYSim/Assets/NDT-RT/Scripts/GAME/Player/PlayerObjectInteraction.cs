using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInteraction : MonoBehaviour
{
    public Camera playerCamera; // Reference to the camera
    public float interactionRange = 3f; // Range for the raycast
    public float objectFloatPoint = -2f;
    private Interactable currentInteractable; // To store the interactable object
    private Interactable lastInteractable; // To store the last interactable object the player looked at
    private bool isHoldingObject = false; // To check if the player is holding an object
    private bool isRotatingObject = false; // To check if the player is rotating an object

    public LayerMask ignoreLayerMask;

    void Update()
    {
        // Cast a ray from the center of the camera
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // Check if the ray hits an object within the interaction range
        if (Physics.Raycast(ray, out hit, interactionRange, ignoreLayerMask))
        {
            // Check if the object has an Interactable component
            Interactable interactable = hit.transform.GetComponent<Interactable>();

            if (interactable != null)
            {
                // If the player looks at a new interactable object, show its tooltip
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.HideTooltip(); // Hide the tooltip of the previous object
                    }

                    interactable.ShowTooltip(); // Show the tooltip of the new object
                    currentInteractable = interactable; // Update the current interactable
                }

                // If the left mouse button is pressed, grab the object
                if (Input.GetMouseButtonDown(0))
                {
                    isHoldingObject = true;
                }
            }
            else
            {
                // If the player is not looking at an interactable object, hide the tooltip
                if (currentInteractable != null)
                {
                    currentInteractable.HideTooltip();
                    currentInteractable = null;
                }
            }
        }
        else
        {
            // If the ray doesn't hit anything, hide the tooltip
            if (currentInteractable != null)
            {
                currentInteractable.HideTooltip();
                currentInteractable = null;
            }
        }

        // If the player is holding an object, update its position
        if (isHoldingObject && currentInteractable != null)
        {
            // Keep the object at a fixed distance from the camera
            currentInteractable.transform.position = ray.GetPoint(interactionRange + objectFloatPoint);
            currentInteractable.GetComponent<Rigidbody>().isKinematic = true;

            // Check for rotation input
            if (Input.GetKeyDown(KeyCode.R))
            {
                isRotatingObject = true;
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                isRotatingObject = false;
            }

            // Rotate the object if the R key is held down
            if (isRotatingObject)
            {
                RotateHeldObject();
            }

            // Release the object when the mouse button is released
            if (Input.GetMouseButtonUp(0))
            {
                isHoldingObject = false;
                isRotatingObject = false;
                currentInteractable.GetComponent<Rigidbody>().isKinematic = false;
                currentInteractable = null;
            }
        }
    }

    // Method to rotate the held object based on mouse movement
    void RotateHeldObject()
    {
        float rotationSpeed = 100f;
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotate the object around the Y-axis (horizontal mouse movement)
        currentInteractable.transform.Rotate(Vector3.up, -mouseX, Space.World);

        // Rotate the object around the X-axis (vertical mouse movement)
        currentInteractable.transform.Rotate(Vector3.right, mouseY, Space.World);
    }

    public bool IsRotatingObject()
    {
        return isRotatingObject;
    }

    // Draw a gizmo to show the raycast in the Scene view
    void OnDrawGizmos()
    {
        if (playerCamera != null)
        {
            // Cast a ray from the center of the camera
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            // Draw the ray in the Scene view
            Gizmos.color = Color.red; // Set the color of the gizmo
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * interactionRange);

            // Optionally, draw a sphere at the end of the ray to indicate the interaction point
            Gizmos.DrawWireSphere(ray.origin + ray.direction * interactionRange, 0.1f);
        }
    }
}

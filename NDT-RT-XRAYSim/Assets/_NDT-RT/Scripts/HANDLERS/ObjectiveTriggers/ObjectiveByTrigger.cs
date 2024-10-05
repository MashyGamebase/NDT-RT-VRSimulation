using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveByTrigger : MonoBehaviour
{
    [SerializeField] private string _objectiveToTrigger;
    [SerializeField] private LayerMask _objectiveLayer;

    // The bounds of the trigger area (size of the cube)
    [SerializeField] private Vector3 _triggerSize = Vector3.one;
    [SerializeField] private Vector3 _triggerCenter = Vector3.zero;

    // Buffer for Physics.OverlapBoxNonAlloc
    private Collider[] _collidersBuffer = new Collider[10];

    // Count objects inside the trigger
    private int objectsInside = 0;

    private void Start()
    {
        // Check if ObjectivesHandler instance exists
        if (ObjectivesHandler.Instance == null)
        {
            Debug.LogWarning("ObjectivesHandler not found!");
        }
    }

    private void Update()
    {
        // Clear buffer and check for objects inside the trigger area
        int colliderCount = Physics.OverlapBoxNonAlloc(
            transform.position + _triggerCenter,
            _triggerSize / 2,
            _collidersBuffer,
            Quaternion.identity,
            _objectiveLayer
        );

        int newObjectsInside = 0;

        for (int i = 0; i < colliderCount; i++)
        {
            Collider col = _collidersBuffer[i];
            if (col != null)
            {
                newObjectsInside++;
            }
        }

        // If the count of objects inside has changed, update the objective
        if (newObjectsInside != objectsInside)
        {
            int difference = newObjectsInside - objectsInside;
            objectsInside = newObjectsInside;

            // Call ObjectivesHandler to update the objective
            if (ObjectivesHandler.Instance != null)
            {
                ObjectivesHandler.Instance.UpdateObjective(_objectiveToTrigger, difference);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the trigger area in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + _triggerCenter, _triggerSize);
    }
}

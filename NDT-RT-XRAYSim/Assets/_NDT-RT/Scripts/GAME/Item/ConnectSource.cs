using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ConnectSource : MonoBehaviour
{
    public Transform connectParent;
    public Transform connectedTarget;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Nozzle")
        {
            if (other.gameObject.GetComponent<XRGrabInteractable>())
            {
                XRGrabInteractable grab = other.gameObject.GetComponent<XRGrabInteractable>();

                if (grab != null && !grab.isSelected)
                {
                    connectedTarget = other.transform;
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Nozzle")
        {
            if (other.gameObject.GetComponent<XRGrabInteractable>())
            {
                XRGrabInteractable grab = other.gameObject.GetComponent<XRGrabInteractable>();

                if (grab != null && grab.isSelected)
                {
                    connectedTarget = null;
                }
            }
        }
    }

    private void Update()
    {
        if(connectedTarget != null)
        {
            connectedTarget.transform.position = connectParent.transform.position;
            connectedTarget.rotation = Quaternion.identity;
        }
    }
}
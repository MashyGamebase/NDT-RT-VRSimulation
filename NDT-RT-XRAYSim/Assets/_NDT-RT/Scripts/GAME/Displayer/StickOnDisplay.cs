using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StickOnDisplay : MonoBehaviour
{
    public GameObject attachedImage;
    public Transform attachTransform;

    public GameObject dataSheet;
    public GameObject YNInteractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Image"))
        {
            XRGrabInteractable image = other.gameObject.GetComponent<XRGrabInteractable>();
            if(image != null)
            {
                if (!image.isSelected)
                {
                    attachedImage = other.gameObject;
                    attachedImage.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    dataSheet.SetActive(true);
                    YNInteractor.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Image"))
        {
            XRGrabInteractable image = other.gameObject.GetComponent<XRGrabInteractable>();
            if (image != null)
            {
                if (!image.isSelected)
                {
                    attachedImage.gameObject.GetComponent<Rigidbody>().useGravity = true;
                    attachedImage = null;
                }
            }
        }
    }

    private void Update()
    {
        if(attachedImage != null)
        {
            attachedImage.gameObject.transform.position = attachTransform.position;
            attachedImage.gameObject.transform.rotation = attachTransform.rotation;
        }
    }
}
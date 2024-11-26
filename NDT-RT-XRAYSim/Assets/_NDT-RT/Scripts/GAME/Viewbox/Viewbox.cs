using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Viewbox : MonoBehaviour
{
    public StickOnDisplay display;
    public Transform attachTransform;

    public GameObject attachedImage;
    public GameObject YNBox;
    public GameObject Details;

    private void OnTriggerEnter(Collider other)
    {
        if (!display.hanged)
            return;

        if (other.CompareTag("Image"))
        {
            XRGrabInteractable image = other.gameObject.GetComponent<XRGrabInteractable>();
            if (image != null)
            {
                attachedImage = other.gameObject;
                attachedImage.gameObject.GetComponent<Rigidbody>().useGravity = false;
                YNBox.SetActive(true);
                Details.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (attachedImage != null)
        {
            attachedImage.gameObject.transform.position = attachTransform.position;
            attachedImage.gameObject.transform.rotation = attachTransform.rotation;
        }
    }
}

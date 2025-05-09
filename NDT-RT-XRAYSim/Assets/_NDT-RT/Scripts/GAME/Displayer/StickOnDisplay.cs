using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StickOnDisplay : MonoBehaviour
{
    public GameObject attachedImage;
    public Transform attachTransform;

    public bool hanged = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hanged)
            return;

        if (!other.GetComponent<NDTOutputHolder>().canBeStickedOn)
            return;

        if (other.CompareTag("Image"))
        {
            XRGrabInteractable image = other.gameObject.GetComponent<XRGrabInteractable>();
            if(image != null)
            {
                attachedImage = other.gameObject;
                attachedImage.gameObject.GetComponent<Rigidbody>().useGravity = false;
                attachedImage.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                hanged = true;
                StartCoroutine(hangtoDry());
            }
        }
    }

    IEnumerator hangtoDry()
    {
        yield return new WaitForSeconds(6);
        attachedImage.gameObject.GetComponent<Rigidbody>().useGravity = true;
        attachedImage = null;
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
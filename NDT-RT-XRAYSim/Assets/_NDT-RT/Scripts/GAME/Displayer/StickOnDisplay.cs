using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOnDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjectsAttached = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Image")
        {
            other.gameObject.transform.SetParent(transform, false);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;

            gameObjectsAttached.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Image")
        {
            other.gameObject.transform.SetParent(null, false);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            gameObjectsAttached.Remove(other.gameObject);
        }
    }
}
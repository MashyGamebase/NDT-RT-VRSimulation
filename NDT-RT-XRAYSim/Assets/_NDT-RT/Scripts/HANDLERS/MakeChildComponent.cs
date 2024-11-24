using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildComponent : MonoBehaviour
{
    public Transform parent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Specimen")
        {

        }
    }
}
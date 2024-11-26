using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerPosition : MonoBehaviour
{
    private NDTSourceGetImage source => NDTSourceGetImage.Instance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.isPlayerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.isPlayerIn = false;
        }
    }
}
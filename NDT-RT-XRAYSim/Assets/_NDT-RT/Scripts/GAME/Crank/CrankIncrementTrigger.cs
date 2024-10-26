using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankIncrementTrigger : MonoBehaviour
{
    [SerializeField] private NDTSourceGetImage source;
    [SerializeField] private float powerAmp = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Crank")
        {
            source.AddCrankPower(powerAmp);
        }
    }
}
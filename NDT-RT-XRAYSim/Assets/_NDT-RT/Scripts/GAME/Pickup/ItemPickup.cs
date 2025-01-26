using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject VFX;

    public void AttachToPlayer()
    {
        VFX.SetActive(false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDTPersistRotation : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.localRotation = Quaternion.identity;
    }
}

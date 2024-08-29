using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    public GameObject[] ropeSegments; // Assign this in the Inspector or dynamically find it

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = ropeSegments.Length;
    }

    void Update()
    {
        for (int i = 0; i < ropeSegments.Length; i++)
        {
            lineRenderer.SetPosition(i, ropeSegments[i].transform.position);
        }
    }
}

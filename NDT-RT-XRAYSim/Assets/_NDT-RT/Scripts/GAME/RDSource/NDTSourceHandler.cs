using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NDTSourceHandler : MonoBehaviour
{
    [SerializeField] private Transform source; // Reference to the radiation source's Transform
    [SerializeField] private Slider angleSlider; // Reference to the Slider component
    [SerializeField] private Transform pivotPoint; // The point around which the source will rotate
    [SerializeField] private float radius = 5f; // Radius of the arc
    [SerializeField] private float minAngle = -45f; // Minimum angle of the arc
    [SerializeField] private float maxAngle = 45f; // Maximum angle of the arc

    void Start()
    {
        //angleSlider.onValueChanged.AddListener(UpdateSourcePosition);
    }

    private void UpdateSourcePosition(float value)
    {
        if (source != null && pivotPoint != null)
        {
            // Calculate the angle based on the slider value
            float angle = Mathf.Lerp(minAngle, maxAngle, value);

            // Calculate the new position on the arc
            float radian = Mathf.Deg2Rad * angle;
            float x = Mathf.Sin(radian) * radius;
            float y = Mathf.Cos(radian) * radius;

            // Update the source's position relative to the pivot point
            source.position = pivotPoint.position + new Vector3(x, y, 0);
        }
    }
}

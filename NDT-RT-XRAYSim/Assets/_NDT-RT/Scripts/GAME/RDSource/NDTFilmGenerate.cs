using UnityEngine;

public class NDTFilmGenerate : MonoBehaviour
{
    [Range(0.1f,1f)] public float generationPercent = 0.2f;
    [Range(1, 10)] public float generationTime = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Image")
        {
            Debug.LogWarning("Image detected");
            other.gameObject.GetComponent<NDTOutputHolder>().RemoveLayer(generationPercent, generationTime);
        }
    }
}
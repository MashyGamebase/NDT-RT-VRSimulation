using UnityEngine;

public class NDTFilmGenerate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Image")
        {
            Debug.LogWarning("Image detected");
            other.gameObject.GetComponent<NDTOutputHolder>().RemoveLayer();
        }
    }
}
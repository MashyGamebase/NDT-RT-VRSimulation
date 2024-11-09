using UnityEngine;

public class FilmStickOn : MonoBehaviour
{
    public GameObject wrappedFilm;
    public GameObject otherFilm;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Image")
        {
            wrappedFilm.SetActive(true);
            if (otherFilm == null)
            {
                otherFilm = other.gameObject;
                otherFilm.gameObject.SetActive(false);
            }
        }
    }
}
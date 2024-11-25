using UnityEngine;

public class FilmStickOn : MonoBehaviour
{
    public GameObject wrappedFilm;
    public GameObject otherFilm;

    private void OnTriggerEnter(Collider other)
    {
        if (NDTSourceGetImage.Instance.isFilmGenerated)
            return;

        if(other.gameObject.tag == "Image")
        {
            wrappedFilm.SetActive(true);
            if (otherFilm == null)
            {
                otherFilm = other.gameObject;
                otherFilm.gameObject.SetActive(false);
                NDTSourceGetImage.Instance.isFilmOn = true;
            }
        }
    }

    public void DettachFilm()
    {
        wrappedFilm.SetActive(false);
        otherFilm.gameObject.SetActive(true);
        Debug.Log("Dettached image");
    }
}
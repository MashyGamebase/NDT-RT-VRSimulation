using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FilmStickOn : MonoBehaviour
{
    public GameObject wrappedFilm;
    public GameObject otherFilm;

    public Transform IQIAttach;
    public GameObject IQI;

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

        if(other.gameObject.tag == "IQI")
        {
            IQI = other.gameObject;
            other.transform.position = IQIAttach.transform.position;
            other.GetComponent<XRGrabInteractable>().enabled = false;
            other.GetComponent<Rigidbody>().useGravity = false;
            //other.transform.SetParent(IQIAttach, true);
            NDTSourceGetImage.Instance.isIQIOn = true;
        }
    }

    public void DettachFilm()
    {
        wrappedFilm.SetActive(false);
        otherFilm.gameObject.SetActive(true);
        Debug.Log("Dettached image");
    }

    private void Update()
    {
        if(IQI != null)
        {
            IQI.transform.position = IQIAttach.position;
            IQI.transform.rotation = IQIAttach.rotation;
        }
    }
}
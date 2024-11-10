using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GeigerCounter : MonoBehaviour
{
    public Transform radiationSource; // Reference to the radiation source
    public AudioClip lowRadiationClip; // Low radiation audio
    public AudioClip mediumRadiationClip; // Medium radiation audio
    public AudioClip highRadiationClip; // High radiation audio
    public float lowThreshold = 10f; // Distance for low radiation
    public float mediumThreshold = 5f; // Distance for medium radiation
    public float highThreshold = 2f; // Distance for high radiation

    private AudioSource audioSource;
    private AudioClip currentClip;

    private NDTSourceGetImage source => NDTSourceGetImage.Instance;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!source.hasRadiation)
        {
            StopRadiationClip();
            return;
        }

        float distance = Vector3.Distance(transform.position, radiationSource.position);

        // Determine which audio to play based on distance
        if (distance <= highThreshold)
        {
            PlayRadiationClip(highRadiationClip);
        }
        else if (distance <= mediumThreshold)
        {
            PlayRadiationClip(mediumRadiationClip);
        }
        else if (distance <= lowThreshold)
        {
            PlayRadiationClip(lowRadiationClip);
        }
        else
        {
            StopRadiationClip();
        }
    }

    private void PlayRadiationClip(AudioClip clip)
    {
        if (currentClip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
            currentClip = clip;
        }
    }

    private void StopRadiationClip()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            currentClip = null;
        }
    }
}
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GeigerCounter : MonoBehaviour
{
    public Threshold threshold;

    public ScoreKeeper scoreKeeper;

    public Transform radiationSource; // Reference to the radiation source
    public AudioClip lowRadiationClip; // Low radiation audio
    public AudioClip mediumRadiationClip; // Medium radiation audio
    public AudioClip highRadiationClip; // High radiation audio
    public float lowThreshold = 10f; // Distance for low radiation
    public float mediumThreshold = 5f; // Distance for medium radiation
    public float highThreshold = 2f; // Distance for high radiation

    private AudioSource audioSource;
    private AudioClip currentClip;

    public Transform playerTransform;
    public GameObject VFX;

    private NDTSourceGetImage source => NDTSourceGetImage.Instance;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(UpdateAfterSecond());
    }

    private void Update()
    {
        /* It will always play the audio regardless if the source has any radiation or not
        if (!source.hasRadiation)
        {
            StopRadiationClip();
            threshold = Threshold.None;
            return;
        }
        */

        float distance = Vector3.Distance(playerTransform.position, radiationSource.position);

        // Determine which audio to play based on distance
        if (distance <= highThreshold)
        {
            PlayRadiationClip(highRadiationClip);
            threshold = Threshold.High;
        }
        else if (distance <= mediumThreshold)
        {
            PlayRadiationClip(mediumRadiationClip);
            threshold = Threshold.Medium;
        }
        else if (distance <= lowThreshold)
        {
            PlayRadiationClip(lowRadiationClip);
            threshold = Threshold.Low;
        }
        else
        {
            threshold = Threshold.Low;
            StopRadiationClip();
        }
    }

    IEnumerator UpdateAfterSecond()
    {
        // Exposure levels determines how much the player has had exposure
        while (true)
        {
            if (!source.hasRadiation)
            {
                yield return null;
            }
            else
            {
                switch (threshold)
                {
                    case Threshold.High:
                        scoreKeeper.AddExposure(0.05f, 3f);
                        break;
                    case Threshold.Medium:
                        scoreKeeper.AddExposure(0.05f, 2f);
                        break;
                    case Threshold.Low:
                        scoreKeeper.AddExposure(0.05f, 1f);
                        break;
                }
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void AttachToPlayer()
    {
        VFX.SetActive(false);
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

public enum Threshold
{
    None,
    Low,
    Medium,
    High
}
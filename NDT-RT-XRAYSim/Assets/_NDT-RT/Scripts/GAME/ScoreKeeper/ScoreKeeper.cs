using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField]
    private TMP_Text playerScoreText;
    [SerializeField]
    private TMP_Text specimenTestedText;

    // Score System
    private int score;
    private int specimenCompleted;
    [SerializeField] private float scoreMultiplier = 2;

    private NDTSourceGetImage source => NDTSourceGetImage.Instance;

    private void Start()
    {
        LoadScores();
    }

    void LoadScores()
    {
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            playerScoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString("D2");
            score = PlayerPrefs.GetInt("PlayerScore");
        }
        else
        {
            int i = 0;
            playerScoreText.text = i.ToString("D2");
        }

        if (PlayerPrefs.HasKey("SpecimenComplete"))
        {
            specimenTestedText.text = PlayerPrefs.GetInt("SpecimenComplete").ToString("D2");
            specimenCompleted = PlayerPrefs.GetInt("SpecimenComplete");
        }
        else
        {
            int i = 0;
            specimenTestedText.text = i.ToString("D2");
        }
    }

    public void SaveAndStoreScore()
    {
        specimenCompleted += 1;
        PlayerPrefs.SetInt("SpecimenComplete", specimenCompleted);

        // Calculate time-based multiplier
        float elapsedTime = Time.timeSinceLevelLoad;
        float timeMultiplier = Mathf.Max(1f, 10f - elapsedTime); // Example: 10x multiplier if completed instantly, reducing to 1x after 10 seconds.

        // Ensure multiplier is positive
        timeMultiplier = Mathf.Clamp(timeMultiplier, 1f, 10f); // Adjust the range as needed.

        int pointsPerSpecimen = (source.SelectedSpecimen % 2 == 0) ? 20 : 5;
        int scoreToAdd = Mathf.FloorToInt(pointsPerSpecimen * scoreMultiplier * timeMultiplier);

        score += scoreToAdd;
        PlayerPrefs.SetInt("PlayerScore", score);
        PlayerPrefs.Save();
    }
}
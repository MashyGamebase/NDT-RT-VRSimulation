using DG.Tweening.Core.Easing;
using System;
using UnityEngine;

public class ObjectivesHandler : MonoBehaviour
{
    // Singleton instance
    public static ObjectivesHandler Instance { get; private set; }

    // ScriptableObject reference to hold objectives information
    [SerializeField] private Objectives objectives;

    // Reference to the GameManager script
    [SerializeField] private GameManager gameManager;

    [Header("Objectives Display")]
    public GameObject objectivePF;

    private void Awake()
    {
        // Implement the singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep this object persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        InitializeObjectives();
    }

    private void InitializeObjectives()
    {
        foreach(var objective in objectives.objectiveList)
        {
            GameObject obj = Instantiate(objectivePF, new Vector3(0,0, -0.2f), Quaternion.identity);
            obj.GetComponent<ObjectiveDataContainer>().objectiveData = objective;
            objective.isCompleted = false;
            objective.currentCompletedGoal = 0;
            obj.transform.SetParent(gameObject.transform, false);
        }
    }

    // Method to update the current completed goal of an objective
    public void UpdateObjective(string objectiveName, int amount)
    {
        // Find the objective in the objectives list
        Objective obj = objectives.FindObjectiveByName(objectiveName);
        if (obj != null)
        {
            // Update the current goal
            obj.currentCompletedGoal += amount;

            // Check if the objective is completed
            if (obj.currentCompletedGoal >= obj.objectiveGoal)
            {
                obj.isCompleted = true;
                Debug.Log($"{objectiveName} completed!");

                // Check if all level objectives are completed
                CheckLevelObjectives();
            }
        }
        else
        {
            Debug.LogWarning($"Objective {objectiveName} not found!");
        }
    }

    // Method to check if all level objectives are completed
    private void CheckLevelObjectives()
    {
        if (objectives.AreAllObjectivesCompleted())
        {
            Debug.Log("All objectives completed!");
            gameManager.OnLevelCompleted();
        }
    }
}

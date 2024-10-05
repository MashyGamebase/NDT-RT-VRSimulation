using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveDataContainer : MonoBehaviour
{
    [SerializeField]
    public Objective objectiveData;

    [Header("UI")]
    public TextMeshProUGUI currentGoalsTxt;
    public TextMeshProUGUI targetGoalsTxt;
    public TextMeshProUGUI objectiveDescriptionTxt;

    private void Update()
    {
        if (objectiveData == null) return;
        if (objectiveData.isCompleted)
        {
            currentGoalsTxt.text = objectiveData.objectiveGoal.ToString();
            return;
        }

        currentGoalsTxt.text = objectiveData.currentCompletedGoal.ToString();
        targetGoalsTxt.text = objectiveData.objectiveGoal.ToString();
        objectiveDescriptionTxt.text = objectiveData.objectiveName.ToString();
    }
}
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Objectives", menuName = "ScriptableObjects/Objectives", order = 1)]
public class Objectives : ScriptableObject
{
    public List<Objective> objectiveList;

    public Objective FindObjectiveByName(string name)
    {
        return objectiveList.Find(obj => obj.objectiveName == name);
    }

    public bool AreAllObjectivesCompleted()
    {
        return objectiveList.TrueForAll(obj => obj.isCompleted);
    }

    public void EnsureUniqueNames()
    {
        HashSet<string> names = new HashSet<string>();
        foreach (var obj in objectiveList)
        {
            if (!names.Add(obj.objectiveName))
            {
                obj.objectiveName += "_Duplicate";
            }
        }
    }
}

[System.Serializable]
public class Objective
{
    public string objectiveName;
    public int objectiveGoal;
    public int currentCompletedGoal;
    public bool isCompleted;
}

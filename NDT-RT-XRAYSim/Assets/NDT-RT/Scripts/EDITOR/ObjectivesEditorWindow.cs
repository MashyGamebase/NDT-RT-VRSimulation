using UnityEditor;
using UnityEngine;

public class ObjectivesEditorWindow : EditorWindow
{
    private Objectives objectives;
    private Vector2 scrollPosition;

    [MenuItem("Tools/Objectives Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObjectivesEditorWindow>("Objectives Editor");
    }

    private void OnGUI()
    {
        // Load the ScriptableObject
        objectives = (Objectives)EditorGUILayout.ObjectField("Objectives", objectives, typeof(Objectives), false);

        if (objectives == null)
        {
            EditorGUILayout.HelpBox("Please assign an Objectives ScriptableObject to edit.", MessageType.Info);
            return;
        }

        // Scrollable list of objectives
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        foreach (var objective in objectives.objectiveList)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.LabelField("Objective Name:", objective.objectiveName);
            objective.objectiveGoal = EditorGUILayout.IntField("Goal:", objective.objectiveGoal);
            objective.currentCompletedGoal = EditorGUILayout.IntField("Current Completed:", objective.currentCompletedGoal);
            objective.isCompleted = EditorGUILayout.Toggle("Is Completed:", objective.isCompleted);

            if (GUILayout.Button("Remove Objective"))
            {
                objectives.objectiveList.Remove(objective);
                break;
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Add New Objective"))
        {
            objectives.objectiveList.Add(new Objective { objectiveName = "New Objective" });
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Save Objectives"))
        {
            EditorUtility.SetDirty(objectives);
            AssetDatabase.SaveAssets();
        }
    }
}

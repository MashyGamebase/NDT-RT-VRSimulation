using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class OutputSOEditor : EditorWindow
{
    private string fileName = "New Specimen Description";
    private string specimenName = "";
    private string materialName = "";
    private string dimensionsName = "";
    private string lengthName = "";
    private string standardsName = "";
    private string dwellTimeName = "";

    [MenuItem("Tools/Create OutputData SO")]
    public static void ShowWindow()
    {
        GetWindow<OutputSOEditor>("Create OutputData");
    }
    private void OnGUI()
    {
        GUILayout.Label("Create New OutputData", EditorStyles.boldLabel);

        // Input field for the file name
        fileName = EditorGUILayout.TextField("File Name", fileName);

        // Input fields for OutputData properties
        specimenName = EditorGUILayout.TextField("Specimen Name", specimenName);
        materialName = EditorGUILayout.TextField("Material Name", materialName);
        dimensionsName = EditorGUILayout.TextField("Dimensions Name", dimensionsName);
        lengthName = EditorGUILayout.TextField("Length Name", lengthName);
        standardsName = EditorGUILayout.TextField("Standards Name", standardsName);
        dwellTimeName = EditorGUILayout.TextField("Dwell Time Name", dwellTimeName);

        // Button to create the ScriptableObject
        if (GUILayout.Button("Create ScriptableObject"))
        {
            CreateScriptableObject();
        }
    }

    private void CreateScriptableObject()
    {
        // Define the target folder path
        string folderPath = "Assets/_NDT-RT/Prefabs/Data/OutputDataSO";

        // Ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Create a new instance of OutputData
        OutputData outputData = ScriptableObject.CreateInstance<OutputData>();

        // Assign data to the OutputData fields
        outputData.specimenName = specimenName;
        outputData.materialName = materialName;
        outputData.dimensionsName = dimensionsName;
        outputData.lengthName = lengthName;
        outputData.standardsName = standardsName;
        outputData.dwellTimeName = dwellTimeName;

        // Generate a unique asset path
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/{fileName}.asset");

        // Create and save the asset
        AssetDatabase.CreateAsset(outputData, assetPath);
        AssetDatabase.SaveAssets();

        // Focus on the newly created asset
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = outputData;

        Debug.Log($"ScriptableObject created at {assetPath}");
    }
}
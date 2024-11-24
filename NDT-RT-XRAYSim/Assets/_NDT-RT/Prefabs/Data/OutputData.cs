using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Specimen Description", menuName = "SpecimenData")]
public class OutputData : ScriptableObject
{
    public string specimenName;
    public string materialName;
    public string dimensionsName;
    public string lengthName;
    public string standardsName;
    public string dwellTimeName;
}
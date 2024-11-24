using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutputDataHolder : Singleton<OutputDataHolder>
{
    public TMP_Text specimenName;
    public TMP_Text materialName;
    public TMP_Text dimensionsName;
    public TMP_Text lengthName;
    public TMP_Text standardsName;
    public TMP_Text dwellTimeName;

    public void SetData(string _specimenName, string _materialName, string _dimensionsName, string _lengthName, string _standardsName, string _dwellTimeName)
    {
        specimenName.text = _specimenName;
        materialName.text = _materialName;
        dimensionsName.text = _dimensionsName;
        lengthName.text = _lengthName;
        standardsName.text = _standardsName;
        dwellTimeName.text = _dwellTimeName;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecimenRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> testPieces;

    void Start()
    {
        RandomizeTestPiece();
    }

    void RandomizeTestPiece()
    {
        int rand = Random.Range(0, testPieces.Count);
        testPieces[rand].gameObject.SetActive(true);
    }
}
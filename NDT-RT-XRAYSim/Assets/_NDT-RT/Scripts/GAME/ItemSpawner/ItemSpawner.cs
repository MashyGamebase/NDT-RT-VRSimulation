using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Prefab")]
    [SerializeField] private GameObject itemPF;
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private Transform spawnPosition;

    [ContextMenu("Spawn Items")]
    public void SpawnItem()
    {
        GameObject obj = Instantiate(itemPF, spawnPosition.position, Quaternion.identity);
        obj.GetComponent<ItemObject>()._itemName = RandomizeName();
        obj.GetComponent<ItemObject>()._itemWeight = RandomFloatValue();
        obj.GetComponent<ItemObject>()._itemSize = RandomFloatValue();
        obj.GetComponent<ItemObject>()._itemFlaw = RandomizeFlaw();

        items.Add(obj);
    }

    public void ClearItems()
    {
        foreach(GameObject gameObject in items)
        {
            Destroy(gameObject);
        }

        items.Clear();
    }

    #region HELPER_METHODS
    internal string RandomizeName()
    {
        List<string> names = new List<string>() { 
            "Metal Pipe",
            "Broken Pipe",
            "Rusty Pipe",
            "Water Pipe",
        };

        int rand = Random.Range(0, names.Count - 1);

        return names[rand];
    }

    internal float RandomFloatValue()
    {
        float rand = Random.Range(1, 255);
        return rand;
    }

    internal string RandomizeFlaw()
    {
        List<string> flaws = new List<string>() {
            "None",
            "Simple Wear",
            "Major Wear",
            "Broken",
            "Beyond Repair",
        };

        int rand = Random.Range(0, flaws.Count - 1);

        return flaws[rand];
    }
    #endregion
}
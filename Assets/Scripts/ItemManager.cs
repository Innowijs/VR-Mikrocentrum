using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public List<GameObject> usedItems = new List<GameObject>();
    public GameObject spawningPoint;

    public void addItem(GameObject itemForAdd)
    {
        usedItems.Add(itemForAdd);   
    }
    public void spawnItems()
    {
        foreach (GameObject item in usedItems)
        {
            Instantiate(item, spawningPoint.transform);
        }
    }
}

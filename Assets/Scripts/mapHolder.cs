using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapHolder : MonoBehaviour {

    RaycastHit hit;
    public bool inRange = false;
    public bool inHand = false;
    public GameObject handSpawnPoint;
    public bool test;
    public bool testdrop;
   // public GameObject bigMap;
    public GameManager manager;
    bool isGreen = false;
    public GameObject map;
    public GameObject mapSpawn;
    void Start () {
        manager.lineCheck.Add(isGreen);
	}

    // Update is called once per frame
    void Update() {
            if ((OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger )|| test) && inRange)
            {
                map.GetComponent<Rigidbody>().isKinematic = true;
                map.transform.SetParent(handSpawnPoint.transform);
                inHand = true;
            }
            if ((OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || testdrop) && inHand)
            {
                map.transform.parent = null;
                map.GetComponent<Rigidbody>().isKinematic = false;
            }
        //if (map.GetComponent<mapChecker>().putOnTable)
        //{
        //    putMapOnTable();
        //}
	}
    public void putMapOnTable()
    {
    //    bigMap.SetActive(true);
        map.transform.parent = null;
        map.transform.SetParent(mapSpawn.transform.parent);
        map.GetComponent<Rigidbody>().isKinematic = true;
        map.transform.SetPositionAndRotation(mapSpawn.transform.localPosition, mapSpawn.transform.localRotation);
        map.SetActive(false);
        manager.lineCheck[4] = false;
        if (manager.eigthTutorialStart)
        {
            manager.eigthTutorial();
        }
    }
    public void resetMap()
    {
        test = false;
        inRange = false;
        inHand = false;
       // map.GetComponent<mapChecker>().putOnTable = false;
      //  bigMap.SetActive(false); 
        map.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "mapHoldable"  && manager.eigthTutorialStart)
        {
            manager.lineCheck[4] = true;
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "mapHoldable")
        {
            manager.lineCheck[4] = false;
            inRange = false;
        }
    }
}

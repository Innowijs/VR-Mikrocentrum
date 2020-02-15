using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChecker : MonoBehaviour {

    public GameManager manager;
    public OVRPlayerController controller;
    public GameObject tutorialRoom;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public FuseBoxScript fuseboxScript;
    public AudioSource technicalRoomSound;
    public AudioSource productionRoomSound;

    IEnumerator startPointer()
    {
        yield return new WaitForSecondsRealtime(2f);
        manager.gameObject.GetComponent<LineRenderer>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FreezeFloor")
        {
            controller.EnableLinearMovement = false; 
            foreach (GameObject item in fuseboxScript.allGloves)
            {
                item.SetActive(false);
            }
            manager.resetTutorial();
            manager.makeRangeBigger();
            StartCoroutine("startPointer");
            manager.Controller.SetActive(true);
        }
        if (other.gameObject.name == "tutorial")
        {
           tutorialRoom.SetActive(true);
            StartCoroutine("startPointer");
        }
        else if(other.gameObject.name == "1")
        {
            room3.SetActive(true);
            StartCoroutine("startPointer");
        }
        else if (other.gameObject.name == "2")
        {
            //technical room 
            technicalRoomSound.enabled = true;
            room1.SetActive(true);
            StartCoroutine("startPointer");
        }
        else if (other.gameObject.name == "3")
        {
            //production room 1
            productionRoomSound.enabled = true;
            room2.SetActive(true);
            StartCoroutine("startPointer");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "FreezeFloor")
        {
            controller.EnableLinearMovement = true;
            manager.makeRangeSmaller();
        }
        if (other.gameObject.name == "tutorial")
        {
            tutorialRoom.SetActive(false);
        }
        else if (other.gameObject.name == "1")
        {
            room3.SetActive(false);
        }
        else if (other.gameObject.name == "2")
        {
            technicalRoomSound.enabled = false;
            room1.SetActive(false);
        }
        else if (other.gameObject.name == "3")
        {
            productionRoomSound.enabled = false;
            room2.SetActive(false);
        }
    }
}

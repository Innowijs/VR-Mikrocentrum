using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChecker2 : MonoBehaviour
{
    public GameManager2 manager;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public OVRPlayerController controller;
    public FuseBoxScript2 fuseboxScript;
   // public BoxCollider headLookingCollider;
    public AudioSource technicalRoomSound;
    public AudioSource productionRoomSound;

    IEnumerator startPointer()
    {
        yield return new WaitForSecondsRealtime(2f);
        manager.gameObject.GetComponent<LineRenderer>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "2")
        {
            technicalRoomSound.enabled = true;
            room1.SetActive(true);
            StartCoroutine("startPointer");
        }
        else if (other.gameObject.name == "1")
        {
            room3.SetActive(true);
            StartCoroutine("startPointer");
        }
        else if (other.gameObject.name == "3")
        {
            productionRoomSound.enabled = true;
            room2.SetActive(true);
            StartCoroutine("startPointer");
        }
        else if (other.gameObject.name == "canOpenDoor")
        {
            fuseboxScript.canOpenDoor = true;
        }
        else if (other.gameObject.name == "FreezeFloor")
        {
            controller.EnableLinearMovement = false;
            foreach (GameObject item in fuseboxScript.allGloves)
            {
                item.SetActive(false);
            }
            StartCoroutine("startPointer");
            manager.Controller.SetActive(true);
            manager.distance2 = 10f;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "1")
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
        else if (other.gameObject.name == "canOpenDoor")
        {
            fuseboxScript.canOpenDoor = false;
        }
    }
}

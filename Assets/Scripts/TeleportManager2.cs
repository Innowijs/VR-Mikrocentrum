using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TeleportManager2 : MonoBehaviour
{
    RaycastHit hit;
    public GameManager2 manager;
    public GameObject locations;
    public OVRScreenFade fadeScript;
    public GameObject centerEyeAnchor;
    public bool test;
    public bool isGreen;
    public GameObject menuLocation;
    public FuseBoxScript2 fuseScript;
    public List<GameObject> mistakesNumbers;
    public GameObject backToScene1;
    public Material normalMat;
    public Material highlighted;
    private void Start()
    {
        manager.lineCheck.Add(isGreen);
    }


    public GameObject bubbleWithText;
    public TextMeshPro bubbleText;
    public void CheckText(string name)
    {
        if (name == "3")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Productielijn 2";
        }
        else if (name == "2")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Technische ruimte";
        }
    }
    bool blockRaycast;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance2))
        {
            if (blockRaycast)
            {
                return;
            }
            if (hit.transform.parent.name == "Teleports")
            {

                CheckText(hit.transform.name);
                manager.lineCheck[1] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    BlockRaycast();
                    manager.lineCheck[1] = false;
                    foreach (Transform specificLocation in locations.transform)
                    {
                        if (hit.transform.name == specificLocation.name)
                        {
                            Teleport(specificLocation.gameObject);
                            break;
                        }
                    }
                }
            }
            else if (hit.transform.name == "EndAssignment")
            {
                manager.lineCheck[1] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    // remove all equipped items;
                    BlockRaycast();
                    fuseScript.SetControllerOn();
                    test = false;
                    Teleport(menuLocation);
                    fuseScript.EndAssignment();
                    //FeedbackMenu();
                    checkMistakes();
                }
            }
            else if (hit.transform.name == "Exit")
            {
                manager.lineCheck[1] = true;
                backToScene1.GetComponent<MeshRenderer>().material = highlighted;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                { 
                    test = false;
                    Debug.Log("QUITTING");
                    Application.Quit();
                }
            }
            else
            {
                backToScene1.GetComponent<MeshRenderer>().material = normalMat;
                manager.lineCheck[1] = false;
                bubbleWithText.SetActive(false);
            }

        }
        else
        {
            backToScene1.GetComponent<MeshRenderer>().material = normalMat;
            manager.lineCheck[1] = false;
            bubbleWithText.SetActive(false);
        }
    }

    public void BlockRaycast()
    {
        blockRaycast = true;
        Invoke("UnblockRaycast",4f);
    }
    
    public void UnblockRaycast()
    {
        blockRaycast = false;
    }

    //public void FeedbackMenu()
    //{
    //    foreach (GameObject item in menuInitial)
    //    {
    //        item.SetActive(false);
    //    }
    //    foreach (GameObject item in menuAssignments)
    //    {
    //        item.SetActive(false);
    //    }
    //    foreach (GameObject item in menuFeedback)
    //    {
    //        item.SetActive(true);
    //    }
    //    return;
    //}

    List<string> mistakesNumbersList;
    public void checkMistakes()
    {
        mistakesNumbersList = fuseScript.mistakes;
        int counter = 0;
        if (mistakesNumbersList.Count == 0)
        {
            return;
        }
        foreach (GameObject item in mistakesNumbers)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in mistakesNumbers)
        {
            if (counter == mistakesNumbersList.Count - 1)
            {
                item.SetActive(true);
                foreach (Transform text in item.transform)
                {
                    text.GetComponent<TextMesh>().text = mistakesNumbersList[counter];
                }
                return;
            }
            item.SetActive(true);
            foreach (Transform text in item.transform)
            {
                text.GetComponent<TextMesh>().text = mistakesNumbersList[counter];
            }
            counter++;
        }
    }

    public void Teleport(GameObject locationPoint)
    {
        if (locationPoint.name == "0") {
            //manager.gameObject.GetComponent<LineRenderer>().enabled = false;
            //StartCoroutine(fadeScript.TeleportScene1(0, 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;

        }
        StartCoroutine(fadeScript.Teleport(0, 1, locationPoint));
        manager.gameObject.GetComponent<LineRenderer>().enabled = false;
       
    }
}

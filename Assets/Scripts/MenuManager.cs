using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    RaycastHit hit;
    public List<GameObject> menuAssignments;
    public List<GameObject> menuInitial;
    public List<GameObject> menuFeedback;
    public List<GameObject> mistakesNumbers;
    public GameManager manager;
    public TeleportationManager teleportManager;
    public GameObject tutorialLocation;
    bool isGreen = false;
    public Material highlighted;
    public Material normalMat;
    public GameObject assignmentLocation;
    public GameObject menuLocation;
    public bool test;
    public MistakesScript mistakeScript;
    public FuseBoxScript fuseboxScript;

    private void Start()
    {
        manager.lineCheck.Add(isGreen);   
    }

    bool blockRaycast;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance))
        {
            if (blockRaycast)
            {
                Debug.Log("blocking all raycasts in this script");
                return;
            }
            if (hit.transform.parent.name == "Menus")
            {
                manager.lineCheck[2] = true;
                hit.transform.gameObject.GetComponent<MeshRenderer>().material = highlighted;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    if (hit.transform.name == "Assignments")
                    {
                        GetAssignmentsMenu();
                    }
                    if (hit.transform.name == "Back")
                    {
                        GetInitialMenu();
                    }
                    if (hit.transform.name == "Exit")
                    {
                        Application.Quit();
                    }
                    if (hit.transform.name == "Sound")
                    {
                        fuseboxScript.phoneRing.Play(1);
                    }
                    if (hit.transform.name == "Tutorial")
                    {
                        StartTutorial();
                    }
                    if (hit.transform.name == "Assignment1")
                    {
                        StartAssignment();
                    }
                    if (hit.transform.name == "Assignment2")
                    {
                        StartAssignment2();
                    }
                    if (hit.transform.name == "Assignment1R")
                    {
                        RestartScene();
                    }
                }
            }
            else if(hit.transform.name == "EndAssignment")
            {
                manager.lineCheck[2] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    BlockRaycast();
                    test = false;
                    teleportManager.Teleport(menuLocation);
                    fuseboxScript.SetControllerOn();
                    fuseboxScript.EndAssignment();
                    FeedbackMenu();
                    checkMistakes();   
                }
            }
            else
            {
                manager.lineCheck[2] = false;
                foreach (GameObject item in menuInitial)
                {
                    item.GetComponent<MeshRenderer>().material = normalMat;
                }
                foreach (GameObject item in menuAssignments)
                {
                    item.GetComponent<MeshRenderer>().material = normalMat;
                }
            }
        }
        else
        {
            manager.lineCheck[2] = false;
        }
    }

    public void BlockRaycast()
    {
        blockRaycast = true;
        Invoke("UnblockRaycast", 4f);
    }

    public void UnblockRaycast()
    {
        blockRaycast = false;
    }

    public void FeedbackMenu()
    {
        foreach (GameObject item in menuInitial)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in menuAssignments)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in menuFeedback)
        {
            item.SetActive(true);
        }
        return;
    }
    public void checkMistakes()
    {
        int counter = 0;
        if (mistakeScript.mistakes.Count == 0)
        {
            return;
        }
        foreach (GameObject item in mistakesNumbers)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in mistakesNumbers)
        {
            if (counter == mistakeScript.mistakes.Count -1)
            {
                item.SetActive(true);
                foreach (Transform text in item.transform)
                {
                    text.GetComponent<TextMesh>().text = mistakeScript.mistakes[counter];
                }
                return;
            }  
            item.SetActive(true);
            foreach (Transform text in item.transform)
            {
                text.GetComponent<TextMesh>().text = mistakeScript.mistakes[counter];
            }
            counter++;
        }
    }
    public void StartAssignment()
    {
        teleportManager.Teleport(assignmentLocation);
    }
    public void StartAssignment2()
    {
        teleportManager.LoadAssignment2();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        teleportManager.Teleport(assignmentLocation);
    }
    public void StartTutorial()
    {
        teleportManager.Teleport(tutorialLocation);
        manager.tutorial = true;
    }
  
    public void GetAssignmentsMenu()
    {
        foreach (GameObject item in menuFeedback)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in mistakesNumbers)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in menuInitial)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in menuAssignments)
        {
            item.SetActive(true);
        }
      
        return;
    }
    public void GetInitialMenu()
    {
        foreach (GameObject item in menuAssignments)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in menuFeedback)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in mistakesNumbers)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in menuInitial)
        {
            item.SetActive(true);
        }
        return;
    }
}

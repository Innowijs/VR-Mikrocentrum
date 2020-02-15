using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class tutorialScript : MonoBehaviour
{
    RaycastHit hit;
    public GameManager manager;
    public GameObject locations;
    public GameObject tutorialRoom;
    private OVRScreenFade fadeScript;
    public GameObject centerEyeAnchor;
    public bool test = false;
    bool isGreen = false;
    public GameObject bubbleWithText;
    public TextMeshPro bubbleText;
    public bool startTeleporting;
    void Awake()
    {
        manager.tutorial = true;
    }

    void Start()
    {
        fadeScript = centerEyeAnchor.GetComponent<OVRScreenFade>();
        manager.lineCheck.Add(isGreen);
    }

    public void CheckText(string name)
    {
        if (name == "0")
        {
            manager.ControllerGuide[9].SetActive(false);
            manager.Controller.SetActive(true);
            bubbleWithText.SetActive(true);
            bubbleText.text = "Main Menu";
        }
    }

    void Update()
    {
        if (startTeleporting)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance))
            {

                if (hit.transform.parent.name == "Teleports")
                {
                    manager.lineCheck[1] = true;
                    CheckText(hit.transform.name);
                    if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
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
                else
                {
                    bubbleWithText.SetActive(false);
                    manager.Controller.SetActive(false);
                    manager.ControllerGuide[9].SetActive(true);
                    manager.lineCheck[1] = false;
                }

            }
            else
            {
                bubbleWithText.SetActive(false);
                manager.Controller.SetActive(false);
                manager.ControllerGuide[9].SetActive(true);
                manager.lineCheck[1] = false;
            }
        }
    }
    public void Teleport(GameObject locationPoint)
    {
        StartCoroutine(fadeScript.Teleport(0, 1, locationPoint));
        manager.gameObject.GetComponent<LineRenderer>().enabled = false;
    }
}

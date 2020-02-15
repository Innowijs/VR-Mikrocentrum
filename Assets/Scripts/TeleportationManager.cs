using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TeleportationManager : MonoBehaviour {

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
    void Start () {
        fadeScript = centerEyeAnchor.GetComponent<OVRScreenFade>();
        manager.lineCheck.Add(isGreen);
    }

    public void CheckText(string name)
    {
        if (name == "3")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Productielijn 1";
        }
        else if (name == "2")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Technische ruimte";
        }
        else if (name == "1")
        {   
            bubbleWithText.SetActive(true);
            bubbleText.text = "Laagspanningsruimte";
        }
        else if (name == "0")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Main Menu";
        }
    }

    bool blockRaycast;
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance))
        {
            if (blockRaycast)
            {
                return;
            }
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
                manager.lineCheck[1] = false;
                }
            
        }
        else
        {
            bubbleWithText.SetActive(false);
            manager.lineCheck[1] = false;
        }
    }
    public void Teleport(GameObject locationPoint)
    {
        BlockRaycast();
        StartCoroutine(fadeScript.Teleport(0, 1, locationPoint));
        manager.gameObject.GetComponent<LineRenderer>().enabled = false;
    }
    public void LoadAssignment2()
    {

        manager.gameObject.GetComponent<LineRenderer>().enabled = false;
        StartCoroutine(fadeScript.TeleportAssignment2(0, 1));
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
}

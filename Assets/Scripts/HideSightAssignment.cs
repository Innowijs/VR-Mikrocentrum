using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSightAssignment : MonoBehaviour {

    public GameObject paper;
    public GameObject gameManager;
    public bool inRange=false;
    bool greenLine = false;
    public bool test = false;
    private void Start()
    {
        gameManager = GameObject.Find("OculusGoControllerModel");
        gameManager.GetComponent<GameManager>().lineCheck.Add(greenLine);
    }
    private void Update()
    {
        if (inRange)
        {
            gameManager.GetComponent<GameManager>().lineCheck[7] = true;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
            {
                test = false;
                inRange = false;
                gameManager.GetComponent<GameManager>().lineCheck.RemoveAt(7);
                paper.SetActive(false);
            }
        }
        else
        {
            gameManager.GetComponent<GameManager>().lineCheck[7] = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OculusGoControllerModel")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "OculusGoControllerModel")
        {
            inRange = false;
        }
    }
}

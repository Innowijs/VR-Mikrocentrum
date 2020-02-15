using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSightAssignment2 : MonoBehaviour
{
    public GameObject paper;
    public GameObject gameManager2;
    public bool inRange = false;
    bool greenLine = false;
    public bool test = false;
    private void Start()
    {
        gameManager2.GetComponent<GameManager2>().lineCheck.Add(greenLine);
    }
    private void Update()
    {
        if (inRange)
        {
            gameManager2.GetComponent<GameManager2>().lineCheck[1] = true;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
            {
                test = false;
                inRange = false;
                gameManager2.GetComponent<GameManager2>().lineCheck.RemoveAt(1);
                paper.SetActive(false);
            }
        }
        else
        {
            gameManager2.GetComponent<GameManager2>().lineCheck[1] = false;
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

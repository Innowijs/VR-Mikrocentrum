using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {

    public GameManager manager;

    bool pullable;
    bool leverPulled;
    public bool test;
    public bool leverOn;
    float offset = 0;
    float angle;
    float angleClamp;  
    float currentX;
    float razlika;
    Vector3 currentLever;
    public GameObject Controller;   
    public GameObject lever;
    public Material greenMat;
    public Material blackMat;
    bool isGreen = false;
   public bool testReset=false;
    void Start () {
        manager.lineCheck.Add(isGreen);
	}
    public void PullLever()
    {
        if (pullable == true && (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test))
        {
            offset = Controller.transform.position.y;
            currentX = currentLever.x;
            leverPulled = true;
            Debug.Log(currentX);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            leverPulled = false;
        }
        if (leverPulled)
        {
            // tova moje da otide v nai gorniq IF >>>
            razlika = offset - Controller.transform.position.y;
            angle = -45 - (razlika * 400);
            angleClamp = Mathf.Clamp(angle, -135, -45);
            currentLever.Set(angleClamp, 0, 0);
            lever.transform.eulerAngles = currentLever;
        }
        if (angleClamp == -45)
        {
            leverOn = false;
        }
        else if (angleClamp == -135)
        {
            leverOn = true;
        }
    }
    public void resetLever()
    {
        lever.transform.eulerAngles = new Vector3(-45f,0,0);
        angleClamp = -45;
        leverOn = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Handle")
        {
            manager.lineCheck[3] = true;
            other.gameObject.GetComponent<MeshRenderer>().material = greenMat;
            pullable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Handle")
        {
            manager.lineCheck[3] = false;
            other.gameObject.GetComponent<MeshRenderer>().material = blackMat;
            pullable = false;
        }
    }
}

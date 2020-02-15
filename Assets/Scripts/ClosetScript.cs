using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetScript : MonoBehaviour {

    public GameManager manager; 
    public GameObject RightDoor;
    public GameObject LeftDoor;
    bool RightPullable = false;
    bool LeftPullable = false;
    bool RightPulling = false;
    bool LeftPulling = false;
    float RightOffset;
    float LeftOffset;
    float RightRazlika;
    float LeftRazlika;
    float RightAngli;
    float LeftAngle;
    float RightAngleClamp;
    float LeftAngleClamp;
    Vector3 RightDoorRotation;
    Vector3 LeftDoorRotation;
   public GameObject controller;
    bool isGreen = false;
    public bool test;

    private void Start()
    {
        manager.lineCheck.Add(isGreen);
    }
    private void Update()
    {
        OpenDoor();
    }
    public void OpenDoor() {

        if (RightPullable == true && (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test))
        {
            RightOffset = controller.transform.position.x;
            RightPulling = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            RightPulling = false;
        }
        if (RightPulling) // -90 - 0  
        {
            RightRazlika = RightOffset - controller.transform.position.x;
            RightAngli = -RightRazlika * 300;
            RightAngleClamp = Mathf.Clamp(RightAngli, -90, 0);
            RightDoorRotation.Set(0, RightAngleClamp, 0);
            RightDoor.transform.eulerAngles = RightDoorRotation;
        }


        if (LeftPullable == true && (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)||test))
        {
            LeftOffset = controller.transform.position.x;
            LeftPulling = true;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            LeftPulling = false;
        }
        if (LeftPulling) // 0-90  
        {
            LeftRazlika = LeftOffset - controller.transform.position.x;
            LeftAngle = -LeftRazlika * 300;
            LeftAngleClamp = Mathf.Clamp(LeftAngle, 0, 90);
            LeftDoorRotation.Set(0, LeftAngleClamp, 0);
            LeftDoor.transform.eulerAngles = LeftDoorRotation;
        }
        if(LeftAngleClamp >= 70)
        {
            if (manager.fifthTutorialStart)
            {
                manager.FifthTutorial();
                gameObject.GetComponent<ClosetScript>().enabled = false;
            }
        }
    }
    public void resetCloset()
    {
        RightDoor.transform.eulerAngles = new Vector3(0, 0, 0);
        LeftDoor.transform.eulerAngles = new Vector3(0, 0, 0);
        RightAngleClamp = 0;
        LeftAngleClamp = 0;
        gameObject.GetComponent<ClosetScript>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.name == "PivotR")
        {
            manager.lineCheck[3] = true;
            RightPullable = true;
        }
        if(other.gameObject.name == "PivotL")
        {
            manager.lineCheck[3] = true;
            LeftPullable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.name == "PivotR")
        {
            manager.lineCheck[3] = false;
            RightPullable = false;
        }
        if (other.gameObject.name == "PivotL")
        {
            manager.lineCheck[3] = false;
            LeftPullable = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    
    RaycastHit hit;
    public LineRenderer line;
   
    public Material lineRed;
    public Material lineGreen;
    public List<GameObject> ControllerGuide;

    public List<GameObject> helmetActive;
    public List<GameObject> helmetFaded;

    public GameObject activeMap;
    public GameObject fadedMap;
    public tutorialScript teleport;
    public GameObject cameraRig;
    public checkPlayerTable toolboardChecker;
    public checkPlayer closetChecker;
    public GameObject Controller;
    public GameObject handWithMap;
    public List<bool> lineCheck; 
    public bool tutorial;
    public bool test = false;
    bool isGreen = false;
    public bool headsetOn = false;

    public bool fourthTutorialStart = false; 
    public bool fifthTutorialStart = false;
    bool sixthStart = false;
    public bool seventhStart = false;
    public bool eigthTutorialStart = false;
    public bool nineTutorialStart = false;
    bool firsthalfOnce =true;
    bool thirdOnce = true;
    bool secondOnce = true;
    bool fifthOnce=true;
    bool sixthOnce = true;
    bool onceeight = true;
    public bool seventhOnce = true;
    public ClosetScript closetScript;
    public List<Light> lightInTheRoom;
    bool onceFirst = true;
    public float distance = 10f;
    public bool kneel = false;
    bool standUpStart = false;
    bool standOnce = true;
    bool mapOn=false;
    float timer = 4f;
    public bool takeMapOnce=true;
    public bool kneelUp = false;
    public GameObject map;
    private void Start()
    {
        cameraRig.GetComponent<Transform>().position = new Vector3(cameraRig.GetComponent<Transform>().position.x, 2.1f, cameraRig.GetComponent<Transform>().position.z);
        lineCheck.Add(isGreen);
    }
    private void LateUpdate()
    {
        foreach (bool item in lineCheck)
        {
            if (item == true)
            {
                line.material = lineGreen;
                break;
            }
            line.material = lineRed;
        }
    }
    void Update () {
        if (tutorial)
        {
            if (onceFirst)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    FirstTutorial();
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
        {
            if (hit.transform.parent.name == "PickUpObjectsTutorial")
            {
                lineCheck[0] = true;
                if (hit.transform.name == "Safety_Helmet")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        UseHeadset();
                        if (secondOnce)
                        {
                            secondOnce = false;
                            SecondTutorial();
                        }
                    }
                }
                else if (hit.transform.name == "mapHoldable")
                {
                    lineCheck[0] = true;
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        if (takeMapOnce == true)
                        {
                            takeMap();
                        }
                    }
                }
            }
            else
            {
                lineCheck[0] = false;
            }
        }     
        else
        {
            lineCheck[0] = false;
        }
        if (OVRInput.GetDown(OVRInput.Button.DpadDown) || kneel)
        {
            cameraRig.GetComponent<Transform>().position = new Vector3(cameraRig.GetComponent<Transform>().position.x, 1.4f, cameraRig.GetComponent<Transform>().position.z);
            if (sixthStart)
            {
                if (sixthOnce)
                {
                    sixthOnce = false;
                    SixthTutorial();
                }
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.DpadUp) || kneelUp)
        {
            cameraRig.GetComponent<Transform>().position = new Vector3(cameraRig.GetComponent<Transform>().position.x, 2.1f, cameraRig.GetComponent<Transform>().position.z);
            if (standUpStart)
            {
                if (standOnce)
                {
                    standOnce = false;
                    SeventhTutorial();
                }
            }
        }
    }
   
    public void putMaskOn()
    {
        if (headsetOn == true)
        {
            foreach (Light light in lightInTheRoom)
            {
                light.color = new Color32(121, 255, 150, 255);
            }
        }
        else if (headsetOn == false)
        {
            foreach (Light light in lightInTheRoom)
            {
                light.color = Color.white;
            }
        }
    }

    public void UseHeadset()
    {
        if (headsetOn == true)
        {
            if (thirdOnce && tutorial)
            {
                thirdOnce = false;
                ThirdTutorial();
            }
            headsetOn = false;
            foreach (GameObject helmet in helmetActive)
            {
                helmet.gameObject.SetActive(true);
            }
            foreach (GameObject helmet in helmetFaded)
            {
                helmet.gameObject.SetActive(false);
            }
            putMaskOn();
        }
        else if (headsetOn == false)
        {
         
            headsetOn = true;
            foreach (GameObject helmet in helmetActive)
            {
                helmet.gameObject.SetActive(false);
            }
            foreach (GameObject helmet in helmetFaded)
            {
                helmet.gameObject.SetActive(true);
            }
            putMaskOn();
        }
    }
    public void makeRangeBigger()
    {
        distance = 10;
    }
    public void makeRangeSmaller()
    {
        distance = 3;
    }
    
    public void FirstTutorial()
    {
        onceFirst = false;
        line.enabled = false;
        line.SetPosition(0, new Vector3(-0.02f, 0, 0.17f)); // position for guides
        line.SetPosition(1, new Vector3(-0.02f, 0, 0.76f));
        Controller.SetActive(false);
        ControllerGuide[0].SetActive(true);
    }
    public void FirstHalfTutorial()
    {
        line.enabled = true;
        firsthalfOnce = false;
        ControllerGuide[0].SetActive(false);
        ControllerGuide[1].SetActive(true);
    }
    public void SecondTutorial()
    {      
        ControllerGuide[1].SetActive(false);
        ControllerGuide[2].SetActive(true);
        
    }
    public void ThirdTutorial()
    {
        ControllerGuide[2].SetActive(false);
        ControllerGuide[3].SetActive(true);
        fourthTutorialStart = true;
    }
    public void FourthTutorial()
    {
        fourthTutorialStart = false;
        ControllerGuide[3].SetActive(false);
        ControllerGuide[4].SetActive(true);
        fifthTutorialStart = true;
    }
    public void FifthTutorial()
    {
        if (fifthOnce)
        {
            fifthOnce = false;
            fifthTutorialStart = false;
            ControllerGuide[4].SetActive(false);
            ControllerGuide[5].SetActive(true);
            sixthStart = true;
        }
    }
    public void SixthTutorial()
    {
        ControllerGuide[5].SetActive(false);
        ControllerGuide[6].SetActive(true);
        standUpStart = true;
    }
    public void SeventhTutorial()
    {
        map.GetComponent<BoxCollider>().enabled = true;
        ControllerGuide[6].SetActive(false);
        ControllerGuide[7].SetActive(true);
        eigthTutorialStart = true;
    }
    public void takeMap()
    {
        if (!mapOn)
        {
            activeMap.SetActive(false);
            fadedMap.SetActive(true);
            mapOn = true;
            handWithMap.SetActive(true);
            ControllerGuide[7].SetActive(false);
        }
        else if (mapOn)
        {
            takeMapOnce = false;
            fadedMap.SetActive(false);
            activeMap.SetActive(true);
            mapOn = false;
            handWithMap.SetActive(false);
            eigthTutorial();
        }
    }
    public void eigthTutorial()
    {
        if (onceeight)
        {
            onceeight = false;
            ControllerGuide[7].SetActive(false);
            ControllerGuide[8].SetActive(true);
        }      
        nineTutorialStart = true;
    }
    public void nineTutorial()
    {
        foreach (GameObject item in ControllerGuide)
        {
            item.SetActive(false);
        }
        //ControllerGuide[9].SetActive(true);
        teleport.startTeleporting = true;
    }
    public void resetTutorial()
    {
       
        if (headsetOn)
        {
            UseHeadset();
        }
        takeMapOnce = true;
        thirdOnce = true;
        fourthTutorialStart = false;
        fifthTutorialStart = false;
        if (tutorial == true)
        {
            teleport.startTeleporting = false;
        }
        tutorial = false;
        closetScript.resetCloset();
        foreach (GameObject item in ControllerGuide)
        {
            item.SetActive(false);
        }
        Controller.SetActive(true);
        line.SetPosition(0, new Vector3(0, 0, 0));
        line.SetPosition(1, new Vector3(0, 0, 0.76f));
        mapOn = false;
        activeMap.SetActive(true);
        fadedMap.SetActive(false);
        //gameObject.GetComponent<mapHolder>().resetMap();
        sixthStart = false;
        seventhStart = false;
        eigthTutorialStart = false;
        nineTutorialStart = false;

        firsthalfOnce = true;
        onceFirst = true;
        standUpStart = false;
        standOnce = true;

        onceeight = true;
        secondOnce = true;
        fifthOnce = true;
        sixthOnce = true;
        seventhOnce = true;
        line.enabled = true;
        toolboardChecker.GetComponent<BoxCollider>().enabled = true;
        closetChecker.GetComponent<BoxCollider>().enabled = true;
    }
}

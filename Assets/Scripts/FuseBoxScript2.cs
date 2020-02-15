using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBoxScript2 : MonoBehaviour
{
    RaycastHit hit;
    public GameObject duspolGlove1;
    public GameObject duspolGlove2;
    public GameObject controller;
    public GameObject screwDriverGlove;
    public GameObject padlockGlove;
    public GameObject fusePullerGlove;
    public GameObject fakeFuseGlove;
    public GameObject multitoolGlove;
    public GameObject multitoolGlove2;
    public GameObject blanketGlove;
    public GameObject isolationGlove;
    public GameObject fuseBlockerGlove;
    public GameObject glove_redwhiteLabel;
    public GameObject hand_redwhiteLabel;
    public GameObject hand_clamp;
    public GameObject hand_duspol1;
    public GameObject hand_duspol2;
    public GameObject hand_blanket;
    public GameObject hand_fusepuller;
    public GameObject hand_fuseblocker;
    public GameObject hand_multimeter1;
    public GameObject hand_multimeter2;
    public GameObject hand_padlock;
    public GameObject hand_phone;
    public GameObject hand_screwdriver;
    public GameObject handSpawnPoint;
    public GameObject glovesClamp;
    public GameObject technicalDrawingGlove;
    public GameObject technicalDrawingHand;
    public GameObject handWithDummyFuse;
    public GameObject handWithSocketWrench;
    public GameObject gloveWithSocketWrench;
    public GameObject handWithDangerSticker;
    public GameObject gloveWithDangerSticker;
    public bool duspolOn = false;
    public bool padlockOn = false;
    public bool screwOn = false;
    public bool fusePullerOn = false;
    public bool multitoolOn = false;
    public bool blanketOn = false;
    public bool isolationOn = false;
    public bool fuseBlockerOn = false;
    public bool technicalDrawingOn = false;
    public bool clampsOn = false;
    public bool gridsOn = false;
    public bool wrenchOn = false;
    public bool dummyFuseOn = false;
    public bool dangerStickerOn = false;
    public List<Transform> allItems;
    public List<GameObject> allGloves;
    public socketsScript socketScript;
    public PickingUp2 PickingUpScript;
    public GameManager2 manager;
    public GameObject mainBoxDangerSticker;
    bool mainBoxDangerStickerOn = false;
    bool isGreen;
    public bool test;
    bool powerSwitchOn = true;
    public GameObject motorCover;
    public GameObject powerSwitch;
    public GameObject goodFuse1;
    public GameObject goodFuse2;
    public GameObject goodFuse3;
    public GameObject badFuse1;
    public GameObject badFuse2;
    public GameObject badFuse3;
    public List<GameObject> bolts;
    bool allBoltsOff = false;
    public PhoneScript phoneScript;
    public List<TextMesh> duspolDisplayAtFuse;
    public TextMesh duspolText;
    public TextMesh HandduspolText;
    public List<GameObject> fuses;
    float timer=3f;
    public bool phoneFirst = false;
    public bool phoneBeforeAnything = true;
    bool coverOff = false;
    string telefon;
    bool showLight = false;
    public bool duspolTested = false;
   public bool checkDuspolSecondTime = false;
    public List<bool> correctMeasures = new List<bool>();
    public Transform redWhiteLabel;
    public AudioSource phoneAudio1;
    public AudioSource phoneAudio2;
    public AudioSource phoneAudio3;
    bool checkSecondPhone = false;
    bool secondPhoneChecked = false;
    public bool measurementsCorrect;
    bool checkThirdPhone;
    bool thirdPhoneChecked;
    public bool ClockReady = false;
    public ClockScript clockScript;
    bool clockWaited = false;
    bool checkClockOnce = true;
    public LayerMask maskDef;
    public List<GameObject> electricParticles;
    public AudioSource electricShock;
    public BoxCollider technicalMapCollider;
    public bool mapLooked4seconds;
    public bool canLookAtMap = true;
    bool canLookAtTechnical = true;
    bool technicalDrawingLooked = false;
    bool cablesDismantled = false;
    public List<BoxCollider> fusesOnMachine;

    public void DeactiveteFusesOnMachine() {
        foreach (BoxCollider item in fusesOnMachine)
        {
            item.enabled = false;
        }
    }
    public void ActiveteFusesOnMachine()
    {
        foreach (BoxCollider item in fusesOnMachine)
        {
            item.enabled = true;
        }
    }
    void Start()
    {
        maskDef = LayerMask.GetMask("Default");
        manager.lineCheck.Add(isGreen);
        for (int i = 0; i < 6; i++)
        {
            correctMeasures.Add(false);
        }
        Debug.Log("how many correct measurements: "+correctMeasures.Count);
    }
    bool startClockOnce = true;
    void Update()
    {
        if (showLight)
        {
            flashLight();
        }
            if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance2, maskDef))
            {
            if (hit.transform.name == "fuseBoxDoor" && dangerStickerOn)
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    PutDangerStickerOnMainBox();
                }
            }
            else if (hit.transform.name == "PowerSwitch")
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    if (startClockOnce)
                    {
                        startClockOnce = false;
                        StartCoroutine(clockScript.StartCountdown());
                    }

                    phoneBeforeAnything = false;
                    canLookAtMap = false;
                    canLookAtTechnical = false;
                    test = false;
                    ClickPowerSwitch();
                }
            }
            else if (hit.transform.parent.name == "Fuses")
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    if (badFuse1.activeSelf == true && badFuse2.activeSelf == true && badFuse3.activeSelf == true && gridsOn)
                    {
                        test = false;
                        PutGridsOnFuses();
                        return;
                    }
                    test = false;
                    phoneBeforeAnything = false;
                    //if (checkClockOnce && ClockReady)
                    //{
                    //    clockWaited = true;
                    //    checkClockOnce = false;
                    //}
                    //else if (checkClockOnce && ClockReady==false)
                    //{
                    //    checkClockOnce = false;
                    //    // ca booom - make something melt somehow :/ :D
                    //}
                    RemoveFuse(hit.transform.name);
                }
            }
            else if (hit.transform.parent.name == "motorCover" && wrenchOn)
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    phoneBeforeAnything = false;
                    RemoveBolt(hit.transform);
                }
            }
            //else if (hit.transform.parent.name == "fuses" && wrenchOn)
            //{
            //    if (hit.transform.name == "4")
            //    {
            //        return;
            //    }
            //    manager.lineCheck[3] = true;
            //    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
            //    {
            //        test = false;
            //        DismantleCables(hit.transform.name);
            //    }
            //}
            else if (hit.transform.name == "motorCover" && allBoltsOff)
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    RemoveMotorCover();
                }
            }
            else if (hit.transform.name == "leverDoor" && !dangerStickerOn)
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    OpenFuseDoor();
                }
            }
            else if (hit.transform.name == "phone")
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    if (phoneFirst == false && phoneBeforeAnything)
                    {
                        phoneAudio1.Play();
                        phoneFirst = true;
                        phoneScript.ActivetePhone(hit.transform);
                        telefon = "first";
                        return;
                    }
                    //else if (secondPhoneChecked == false && checkSecondPhone)
                    //{
                    //    secondPhoneChecked = true;
                    //    phoneAudio2.Play();
                    //    phoneScript.ActivetePhone(hit.transform);
                    //    telefon = "second";
                    //}
                    else if (thirdPhoneChecked == false && checkThirdPhone)
                    {
                        thirdPhoneChecked = true;
                        phoneAudio3.Play();
                        phoneScript.ActivetePhone(hit.transform);
                        telefon = "third";
                    }
                    switch (telefon)
                    {
                        case "first":
                            phoneAudio1.Play();
                            phoneScript.ActivetePhone(hit.transform);
                            break;
                        //case "second":
                        //    phoneAudio2.Play();
                        //    phoneScript.ActivetePhone(hit.transform);
                        //    break;
                        case "third":
                            phoneAudio3.Play();
                            phoneScript.ActivetePhone(hit.transform);
                            break;
                    }
                }
            }
            else if (hit.transform.parent.name == "fuses" && duspolOn)
            {
                manager.lineCheck[3] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    MeasureFuse(hit.transform.name);
                    if (checkClockOnce && ClockReady)// all good
                    {
                        clockWaited = true;
                        checkClockOnce = false;
                    }
                    else if (checkClockOnce && ClockReady == false) // not waited
                    {
                        checkClockOnce = false;
                        electricShock.Play();
                        foreach (GameObject particle in electricParticles)
                        {
                            particle.SetActive(true); // particles effect when wrong move!
                        }
                    }
                }
            }
            else
            {
                manager.lineCheck[3] = false;
            }
        }
        else
        {
            manager.lineCheck[3] = false;
        }
    }

    public GameObject GridsOnFuse;
    public void PutGridsOnFuses()
    {
        if (GridsOnFuse.activeSelf == false)
        {
            GridsOnFuse.SetActive(true);
        }
        else
        {
            GridsOnFuse.SetActive(false);
        }
        takeToolboxItem(redWhiteLabel);
        takeGrids();
    }
    public GameObject normalCables1;
    public GameObject dismantledCables1;
    public GameObject normalCables2;
    public GameObject dismantledCables2;
    public GameObject normalCables3;
    public GameObject dismantledCables3;
    bool cable1Dismantled = false;
    bool cable2Dismantled = false;
    bool cable3Dismantled = false;

    public void DismantleCables(string name)
    {
        if (name == "1")
        {
            if (cable1Dismantled == false)
            {
                cable1Dismantled = true;
                normalCables1.SetActive(false);
                dismantledCables1.SetActive(true);
            }
            else
            {
                cable1Dismantled = false;
                normalCables1.SetActive(true);
                dismantledCables1.SetActive(false);
            }
        }
        else if (name == "2")
        {
            if (cable2Dismantled == false)
            {
                cable2Dismantled = true;
                normalCables2.SetActive(false);
                dismantledCables2.SetActive(true);
            }
            else
            {
                cable2Dismantled = false;
                normalCables2.SetActive(true);
                dismantledCables2.SetActive(false);
            }
        }
        else if (name == "3")
        {
            if (cable3Dismantled == false)
            {
                cable3Dismantled = true;
                normalCables3.SetActive(false);
                dismantledCables3.SetActive(true);
            }
            else
            {
                cable3Dismantled = false;
                normalCables3.SetActive(true);
                dismantledCables3.SetActive(false);
            }
        }
        if (cable3Dismantled && cable3Dismantled && cable2Dismantled)
        {
            Debug.Log("all cables are dismantled");
            cablesDismantled = true;
        }
        else
        {
            Debug.Log("cables are NO!!!! dismantled");
            cablesDismantled = false;
        }
        
    }

    string firstChecked = "";
    string secondChecked = "";
    public List<GameObject> duspolAtFuses;
    public List<GameObject> duspolAtFusesRight;
    public void MeasureFuse(string FuseNumber)
    {
        if (hand_duspol1.activeSelf == true)
        {
            hand_duspol1.gameObject.SetActive(false);
            hand_duspol2.gameObject.SetActive(true);
            firstChecked = FuseNumber;
            foreach (GameObject item in duspolAtFuses)
            {
                if (item.name == FuseNumber)
                {
                    item.SetActive(true);
                }
            }
        }
        else if (hand_duspol2.activeSelf == true)
        {
            secondChecked = FuseNumber;
            foreach (GameObject item in duspolAtFusesRight)
            {
                if (item.name == FuseNumber)
                {
                    item.SetActive(true);
                }
            }
            CheckCombinations(firstChecked, secondChecked);
            firstChecked = "";
            secondChecked = "";
        }
        if (duspolGlove1.activeSelf == true)
        {
            duspolGlove1.gameObject.SetActive(false);
            duspolGlove2.gameObject.SetActive(true);
            firstChecked = FuseNumber;
            foreach (GameObject item in duspolAtFuses)
            {
                if (item.name == FuseNumber)
                {
                    item.SetActive(true);
                }
            }
        }
        else if (duspolGlove2.activeSelf == true)
        {

            secondChecked = FuseNumber;
            foreach (GameObject item in duspolAtFusesRight)
            {
                if (item.name == FuseNumber)
                {
                    item.SetActive(true);
                }
            }
            CheckCombinations(firstChecked, secondChecked);
            firstChecked = "";
            secondChecked = "";
        }
    }
    bool electricityOff = true;

    bool doorOpened = false;
    public GameObject fuseDoor;
    public void OpenFuseDoor()
    {
        if (doorOpened) // closing
        {
            doorOpened = false;
            DeactiveteFusesOnMachine();
            technicalMapCollider.enabled = false;
            fuseDoor.GetComponent<Animator>().SetTrigger("close_door");
        }
        else // opening
        {
            doorOpened = true;
            ActiveteFusesOnMachine();
            technicalMapCollider.enabled = true;
            fuseDoor.GetComponent<Animator>().SetTrigger("open_door");
        }
    }

    public void CheckCombinations(string firstFuse, string secondFuse)
    {
        if ((firstFuse == "1" && secondFuse == "2") || (firstFuse == "2" && secondFuse == "1"))
        {
            if (electricityOff)
            {
                correctMeasures[0] = true;
                displayElectricity("   0");
            }
            else
            {
                displayElectricity("400");
                if (measurementsCorrect)
                {
                    checkDuspolSecondTime = true;
                }
                foreach (bool item in correctMeasures)
                {
                    if (item == true)
                    {
                        //canTestDuspolFirst = false;
                        showLight = true;
                        return;
                    }
                }
                duspolTested = true;
                
            }
        }
        else if ((firstFuse == "1" && secondFuse == "3") || (firstFuse == "3" && secondFuse == "1"))
        {
            if (electricityOff)
            {
                correctMeasures[1] = true;
                displayElectricity("   0");
            }
            else
            {
                displayElectricity("400");
                if (measurementsCorrect)
                {
                    checkDuspolSecondTime = true;
                }
            }         
        }
        else if ((firstFuse == "1" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "1"))
        {
            if (electricityOff)
            {
                correctMeasures[2] = true;
                displayElectricity("   0");
            }
            else
            {
                displayElectricity("400");
                if (measurementsCorrect)
                {
                    checkDuspolSecondTime = true;
                }
            }  
        }
        else if ((firstFuse == "2" && secondFuse == "3") || (firstFuse == "3" && secondFuse == "2"))
        {
            if (electricityOff)
            {
                correctMeasures[3] = true;
                displayElectricity("   0");
            }
            else
            {
                displayElectricity("400");
                if (measurementsCorrect)
                {
                    checkDuspolSecondTime = true;
                }
            }       
        }
        else if ((firstFuse == "2" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "2"))
        {
            if (electricityOff)
            {
                correctMeasures[4] = true;
                displayElectricity("   0");
            }
            else
            {
                displayElectricity("400");
                if (measurementsCorrect)
                {
                    checkDuspolSecondTime = true;
                }
            }      
        }
        else if ((firstFuse == "3" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "3"))
        {
            if (electricityOff)
            {
                correctMeasures[5] = true;
                displayElectricity("   0");
            }
            else
            {
                displayElectricity("400");
                if (measurementsCorrect)
                {
                    checkDuspolSecondTime = true;
                }
            }
        }
        showLight = true;
        checkDuspolFirstTime = true;
        foreach (bool item in correctMeasures)
        {
            if (item == false)
            {
                return;
            }
        }
        Debug.Log("ALL MEASUREMENTS ARE DONE CORRECT");
        measurementsCorrect = true;
        checkThirdPhone = true;
        checkSecondPhone = false;
    }

    public bool checkDuspolFirstTime = false;
    bool canTestDuspolFirst = true;

    public void displayElectricity(string electricity)
    {
        foreach (TextMesh item in duspolDisplayAtFuse)
        {
            item.text = electricity;
        }
        duspolText.text = electricity;
        HandduspolText.text = electricity;
    }

    public void flashLight()
    {
        timer -= Time.deltaTime;
        deactivateFuses();
        if (timer <= 0)
        {
            if (PickingUpScript.glovesOn)
            {
                duspolGlove2.gameObject.SetActive(false);
                duspolGlove1.gameObject.SetActive(true);
                foreach (GameObject item in duspolAtFuses)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in duspolAtFusesRight)
                {
                    item.SetActive(false);
                }
                foreach (TextMesh item in duspolDisplayAtFuse)
                {
                    item.text = "240";
                }
                duspolText.text = "0";
                showLight = false;
                timer = 3;
                activateFuses();
            }
            else if (!PickingUpScript.glovesOn)
            {
                hand_duspol2.gameObject.SetActive(false);
                hand_duspol1.gameObject.SetActive(true);
                foreach (GameObject item in duspolAtFuses)
                {
                    item.SetActive(false);
                }
                foreach (GameObject item in duspolAtFusesRight)
                {
                    item.SetActive(false);
                }
                foreach (TextMesh item in duspolDisplayAtFuse)
                {
                    item.text = "240";
                }
                HandduspolText.text = "0";
                showLight = false;
                timer = 3;
                activateFuses();
            }
        }
    }
    public List<string> mistakes = new List<string>();
    public void EndAssignment()
    {
        if (phoneFirst == false)
        {
            mistakes.Add("1");
        }
        if (technicalDrawingLooked == false)
        {
            mistakes.Add("2");
        }
        if (mapLooked4seconds==false)
        {
            mistakes.Add("3");
        }
        if (padlockAndStickerOnPowerSwitch.activeSelf == false)
        {
            mistakes.Add("4");
        }
        if (mainBoxDangerStickerOn == false)
        {
            mistakes.Add("5");
        }
        if (clockWaited == false)
        {
            mistakes.Add("6");
        }
        if(goodFuse1.activeSelf ==true || goodFuse2.activeSelf == true|| goodFuse3.activeSelf == true)
        {
            mistakes.Add("7");
        }
        if (badFuse1.activeSelf == false || badFuse2.activeSelf == false || badFuse3.activeSelf == false)
        {
            mistakes.Add("8");
        }
        if (GridsOnFuse.activeSelf == false)
        {
            mistakes.Add("9");
        }
        if (duspolTested == false)
        {
            mistakes.Add("10");
        }
        if (measurementsCorrect == false)
        { 
                mistakes.Add("11");
        }
        if (checkDuspolSecondTime==false)
        {
            mistakes.Add("12");
        }
        //if (cablesDismantled == false) {
        //    mistakes.Add("13");
        //}
        if (thirdPhoneChecked == false)
        {
            mistakes.Add("13");
        }
    }

    public void activateFuses()
    {
        foreach (GameObject fuse in fuses)
        {
            fuse.SetActive(true);
        }
    }

    public void deactivateFuses()
    {
        foreach (GameObject fuse in fuses)
        {
            fuse.SetActive(false);
        }
    }

    public void RemoveMotorCover()
    {
        if (coverOff)
        {
            coverOff = false;
            motorCover.GetComponent<Animator>().SetTrigger("returnCover");

        }
        else
        {
            coverOff = true;
            motorCover.GetComponent<Animator>().SetTrigger("removeCover");
        }
    }

    public void RemoveBolt(Transform bolt)
    {
        bolt.gameObject.GetComponent<Animator>().enabled = true;
        Invoke("CheckBolts",2f);
    }

    public void CheckBolts()
    {
        foreach (GameObject item in bolts)
        {
            if (item.activeSelf == true)
            {
                return;
            }
        }
        motorCover.GetComponent<BoxCollider>().enabled = true;
        allBoltsOff = true;
    }

    public void RemoveFuse(string fuse)
    {
        GameObject goodFuse = null;
        GameObject badFuse = null;
        if (fuse == "fuse1")
        {
            goodFuse = goodFuse1;
            badFuse = badFuse1;
        } 
        else if(fuse == "fuse2")
        {
            goodFuse = goodFuse2;
            badFuse = badFuse2;
        }
        else if (fuse == "fuse3")
        {
            goodFuse = goodFuse3;
            badFuse = badFuse3;
        }

        if (goodFuse.activeSelf)
        {
        goodFuse.SetActive(false);
        }
        else if (goodFuse.activeSelf == false && badFuse.activeSelf == false && dummyFuseOn)
        {
        badFuse.SetActive(true);
        }
        else if (goodFuse.activeSelf == false && badFuse.activeSelf == true)
        {
        badFuse.SetActive(false);
        }
        else if (goodFuse.activeSelf == false && badFuse.activeSelf == false)
        {
            goodFuse.SetActive(true);
        }
        if(badFuse1.activeSelf==true && badFuse2.activeSelf == true&& badFuse3.activeSelf == true)
        {
            checkSecondPhone = true;
        }
    }


    public Transform dangerStickers;
    public void PutDangerStickerOnMainBox()
    {
        takeToolboxItem(dangerStickers);
        takeDangerStickers();
        mainBoxDangerStickerOn = true;
        mainBoxDangerSticker.SetActive(true);
        dangerStickerOn = false;
    }
    bool powerWithPadlock = false;
    bool powerWithSticker = false;
    public GameObject padlockOnPowerSwitch;
    public GameObject padlockAndStickerOnPowerSwitch;
    public List<GameObject> padlocksActive;
    public List<GameObject> padlocksFaded;
    public List<GameObject> stickersActive;
    public List<GameObject> stickersFaded;

    public void ClickPowerSwitch()
    {
        if (powerSwitchOn)
        {
            powerSwitchOn = false;
            powerSwitch.transform.localEulerAngles = new Vector3(0, -90, 0);
        }
        else if (powerSwitchOn==false)
        {
            if (powerWithPadlock == false && padlockOn)
            {
                powerWithPadlock = true;
                padlockOnPowerSwitch.SetActive(true);
                padlockOn = false;
                padlockGlove.SetActive(false);
                hand_padlock.SetActive(false);
                foreach (GameObject item in padlocksActive)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in padlocksFaded)
                {
                    item.SetActive(false);
                }
                if (PickingUpScript.glovesOn)
                {
                    PickingUpScript.gloves.SetActive(true);
                }
                else
                {
                    controller.SetActive(true);
                }

            }
            else if (powerWithPadlock && gridsOn)
            {
                padlockOnPowerSwitch.SetActive(false);
                padlockAndStickerOnPowerSwitch.SetActive(true);
                gridsOn = false;
                powerWithSticker = true;
                hand_redwhiteLabel.SetActive(false);
                glove_redwhiteLabel.SetActive(false);
                foreach (GameObject item in stickersActive)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in stickersFaded)
                {
                    item.SetActive(false);
                }
                if (PickingUpScript.glovesOn)
                {
                    PickingUpScript.gloves.SetActive(true);
                }
                else
                {
                    controller.SetActive(true);
                }

            }
            else if (powerWithSticker)
            {
                powerWithSticker = false;
                padlockAndStickerOnPowerSwitch.SetActive(false);
                powerWithPadlock = false;
            }
            else if (powerWithPadlock)
            {
                padlockOnPowerSwitch.SetActive(false);
                powerWithPadlock = false;
            }
            else
            {
                powerWithPadlock = false;
                powerWithSticker = false;
                powerSwitchOn = true;
                powerSwitch.transform.localEulerAngles = new Vector3(0, -90, -90);
            }
        }
    }

    public void pickUpItem()
    {
        foreach (GameObject item in socketScript.leftDuspols)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in socketScript.rightDuspols)
        {
            item.SetActive(false);
        }

        duspolOn = false;
        padlockOn = false;
        screwOn = false;
        fusePullerOn = false;
        multitoolOn = false;
        blanketOn = false;
        isolationOn = false;
        fuseBlockerOn = false;
        technicalDrawingOn = false;
        clampsOn = false;
        gridsOn = false;
        wrenchOn = false;
        dummyFuseOn = false;
        dangerStickerOn = false;
    }

    public void takeDummyFuse()
    {
        if (dummyFuseOn == false)
        {
            pickUpItem();
            dummyFuseOn = true;
            if (!PickingUpScript.glovesOn)
            {
                handWithDummyFuse.SetActive(true);
            }
            else
            {
                fakeFuseGlove.SetActive(true);
            }
        }
        else
        {
            pickUpItem();
            dummyFuseOn = false;
            if (!PickingUpScript.glovesOn)
            {
                controller.SetActive(true);
            }
            else
            {
                PickingUpScript.gloves.SetActive(true);
            }
        }
    }

    public void takeSocketWrench() {
        if (wrenchOn == false)
        {
            pickUpItem();
            wrenchOn = true;
            if (!PickingUpScript.glovesOn)
            {
                handWithSocketWrench.SetActive(true);
            }
            else
            {
                gloveWithSocketWrench.SetActive(true);
            }
        }
        else
        {
            pickUpItem();
            wrenchOn = false;
            if (!PickingUpScript.glovesOn)
            {
                controller.SetActive(true);
            }
            else
            {
                PickingUpScript.gloves.SetActive(true);
            }

        }
    }

    public void takeDangerStickers() {

        if (dangerStickerOn == false)
        {
            pickUpItem();
            dangerStickerOn = true;
            if (!PickingUpScript.glovesOn)
            {
                handWithDangerSticker.SetActive(true);
            }
            else
            {
                gloveWithDangerSticker.SetActive(true);
            }
        }
        else
        {
            pickUpItem();
            dangerStickerOn = false;
            if (!PickingUpScript.glovesOn)
            {
                controller.SetActive(true);
            }
            else
            {
                PickingUpScript.gloves.SetActive(true);
            }

        }
    }

    public void takeGrids()
    {
        if (gridsOn == false)
        {
            pickUpItem();
            gridsOn = true;
            if (PickingUpScript.glovesOn)
            {
                glove_redwhiteLabel.SetActive(true);
            }
            else
            {
                hand_redwhiteLabel.gameObject.SetActive(true);
            }
        }
        else if (gridsOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takeIsolation()
    {
        if (isolationOn == false)
        {
            pickUpItem();
            isolationOn = true;
            isolationGlove.SetActive(true);

        }
        else if (isolationOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takeMultitool()
    {
        if (multitoolOn == false)
        {
            pickUpItem();
            multitoolOn = true;
            if (PickingUpScript.glovesOn)
            {
                multitoolGlove.SetActive(true);
            }
            else
            {
                hand_multimeter1.gameObject.SetActive(true);
            }
        }
        else if (multitoolOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takeFuseBlocks()
    {
        if (fuseBlockerOn == false)
        {
            pickUpItem();
            fuseBlockerOn = true;
            fusePullerOn = true;
            if (PickingUpScript.glovesOn)
            {
                fuseBlockerGlove.SetActive(true);
            }
            else
            {
                hand_fuseblocker.gameObject.SetActive(true);
            }
        }
        else if (fuseBlockerOn)
        {
            pickUpItem();
            fusePullerOn = true;
            if (fuseBlockerGlove.activeSelf)
            {
                fusePullerGlove.SetActive(true);
            }
            else
            {
                hand_fusepuller.gameObject.SetActive(true);
            }

        }
    }

    public void takeBlanket()
    {
        if (blanketOn == false)
        {
            pickUpItem();
            blanketOn = true;
            if (PickingUpScript.glovesOn)
            {
                blanketGlove.SetActive(true);
            }
            else
            {
                hand_blanket.gameObject.SetActive(true);
            }
        }
        else if (blanketOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void SetControllerOn()
    {
        pickUpItem();
        foreach (GameObject gloveItem in allGloves)
        {
            Debug.Log("seeting off all gloves");
            gloveItem.SetActive(false);
        }
        controller.gameObject.SetActive(true);
    }

    public void takeScrewdriver()
    {
        if (screwOn == false)
        {
            pickUpItem();
            screwOn = true;
            if (PickingUpScript.glovesOn)
            {
                screwDriverGlove.SetActive(true);
            }
            else
            {
                hand_screwdriver.gameObject.SetActive(true);
            }
        }
        else if (screwOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takeFusePuller()
    {
        if (fusePullerOn == false)
        {
            pickUpItem();
            fusePullerOn = true;
            if (PickingUpScript.glovesOn)
            {
                fusePullerGlove.SetActive(true);
            }
            else
            {
                hand_fusepuller.SetActive(true);
            }
        }
        else if (fusePullerOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }

    }

    public void takeDuspol()
    {
        if (duspolOn == false)
        {
            pickUpItem();
            duspolOn = true;
            if (PickingUpScript.glovesOn)
            {
                duspolGlove1.gameObject.SetActive(true);
            }
            else
            {
                hand_duspol1.gameObject.SetActive(true);
            }
        }
        else if (duspolOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takePadlock()
    {
        if (padlockOn == false)
        {
            pickUpItem();
            padlockOn = true;
            if (PickingUpScript.glovesOn)
            {
                padlockGlove.SetActive(true);
            }
            else
            {
                hand_padlock.SetActive(true);
            }
        }
        else
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takeClamps()
    {
        if (clampsOn == false)
        {
            pickUpItem();
            clampsOn = true;
            Debug.Log("clamps are ON NOW");
            if (PickingUpScript.glovesOn)
            {
                glovesClamp.SetActive(true);
            }
            else
            {
                hand_clamp.SetActive(true);
            }

        }
        else
        {
            Debug.Log("clamps are OFFFFFFF");
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takeTechnicalDrawing()
    {
        if (technicalDrawingOn == false)
        {
            pickUpItem();
            technicalDrawingOn = true;
            if (canLookAtTechnical) {
                technicalDrawingLooked = true;
            }
            foreach (GameObject item in allGloves)
            {
                item.SetActive(false);
            }
            if (PickingUpScript.glovesOn)
            {
                technicalDrawingGlove.SetActive(true);
            }
            else
            {
                technicalDrawingHand.SetActive(true);
            }
        }
        else if (technicalDrawingOn)
        {
            pickUpItem();
            if (PickingUpScript.glovesOn)
            {
                technicalDrawingGlove.SetActive(false);
                PickingUpScript.gloves.SetActive(true);
            }
            else
            {
                technicalDrawingHand.SetActive(false);
                controller.gameObject.SetActive(true);
            }
        }
    }

    public void takeToolboxItem(Transform item)
    {
        foreach (Transform toolboardItems in allItems)
        {
            foreach (Transform toolBoardItem in toolboardItems)
            {
                if (toolBoardItem.name != item.gameObject.name)
                {
                    if (toolBoardItem.name != "Gloves" && toolBoardItem.name != "Safety_Helmet")
                    {
                        foreach (Transform child in toolBoardItem)
                        {
                            if (child.gameObject.name == "Active")
                            {
                                child.gameObject.SetActive(true);
                            }
                            else if (child.gameObject.name == "Faded")
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else
                {
                    GameObject activeItem = null;
                    GameObject fadedItem = null;

                    foreach (Transform child1 in toolBoardItem)
                    {

                        if (child1.gameObject.name == "Active")
                        {
                            activeItem = child1.gameObject;
                        }
                        else if (child1.gameObject.name == "Faded")
                        {
                            fadedItem = child1.gameObject;
                        }
                    }
                    if (activeItem.activeSelf)
                    {
                        Debug.Log("take item");
                        activeItem.SetActive(false);
                        fadedItem.SetActive(true);
                    }
                    else if (fadedItem.activeSelf)
                    {
                        activeItem.SetActive(true);
                        fadedItem.SetActive(false);
                    }
                }
            }
            foreach (GameObject item2 in socketScript.leftDuspols)
            {
                item2.SetActive(false);
            }
        }
        foreach (GameObject gloveItem in allGloves)
        {
            Debug.Log("seeting off all gloves");
            gloveItem.SetActive(false);
        }
    }
}

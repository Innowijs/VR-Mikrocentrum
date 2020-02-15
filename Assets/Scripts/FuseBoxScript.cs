using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FuseBoxScript : MonoBehaviour
{

    RaycastHit hit;
    RaycastHit hit2;
    public GameManager manager;
    public MistakesScript mistakesScript;
    public bool test;
    public Material standart;
    public Material greenLight;
    public Material redLight;
    public List<GameObject> bolts;
    public GameObject firstSwitch;
    public GameObject padlockSubswitch;
    public List<GameObject> padlocksActive;
    public List<GameObject> padlocksFaded;
    public List<GameObject> duspolAtFuses;
    public List<GameObject> duspolAtFusesRight;
    public List<GameObject> multitoolAtFuses;
    public List<TextMesh> duspolDisplayAtFuse;
    public GameObject coverHotspotArea;
    public GameObject subSwitch;
    public GameObject duspolGlove1;
    public GameObject duspolLight;
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
    public GameObject firstCover;
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
    public Rigidbody handSpawnRg;
    public GameObject glovesClamp;
    public GameObject odeqlo;
    public GameObject isolationFuseBox;
    public GameObject isolationHand;
    public Transform isolation;
    public Transform blanket;
    public Transform redWhiteLabel;
    public GameObject fuseBoxRedWhiteLabel;
    public bool boltsOff = false;
    public bool firstSwitchOn = false;
    private bool isolationOnFuseBox = false;
    bool subSwitchOn = false;
    bool duspolOn = false;
    bool padlockOn = false;
    public bool multitoolOn = false;
    public bool fusePullerOn = false;
    public bool clampsOn = false;
    public bool technicalDrawingOn = false;
    public bool gridsOn = false;
    bool onceAngryChef = true;
    bool checkDuspolFirstTime = true;
    bool checkDuspolAgain = false;
    public GameObject technicalDrawingGlove;
    public GameObject technicalDrawingHand;
    bool fakeFuseOn = false;
    public bool blanketOn = false;
    public bool fuseBlockerOn = false;
    bool isolationOn = false;
    public bool screwOn = false;
    bool isGreen = false;
    bool isGreen2 = false;
    public bool inRange = false;
    bool inHand = false;
    bool showLight = false;
    bool subWithPadlock = false;
    public List<GameObject> fuses;
    public bool dropTest = false;
    public List<GameObject> electricParticles;
    List<string> mistakesTaken;
    public PickingUpScript PickingUpScript;
    public List<Transform> allItems;
    public List<GameObject> allGloves;
    public List<Transform> fuseBlockers;
    public AudioSource phoneRing;
    public AudioSource phoneAngryChef;
    public AudioSource phoneStart;
    public AudioSource phonePermitSound;
    public AudioSource phoneEnd;
    bool phoneFirstPickUp = false;
    bool phoneSecondPickUp = false;
    bool phoneThirdPickUp = false;
    public AudioSource electricShock;
    float timer = 3;
    bool checkDuspolSecondTime = false;
    bool correctPickUpPhone = false;
    bool phoneNotPickedUp = false;
    bool helmetWhenTakingFuses = false;
    public PhoneScript phoneScript;
    bool didSomeOfTheActionsWithGlove = false;
    bool phonePlayedOnce = false;
    bool correctFirstPickUpPhone = false;
    bool phonePermit = false;
    bool phoneAtEnd = false;
    bool phoneBeforeEnd = false;
    bool phoneBeforeOtherStuff = true;
    bool phoneBeforeIsolation = false;
    bool phonePlayChefOnce = false;
    public TextMesh duspolText;
    public TextMesh HandduspolText;
    bool duspolTested = false;
    bool fuseBlock1 = true;
    bool fuseBlock2 = true;
    bool fuseBlock3 = true;
    bool fuseBlock1Blocked = false;
    bool fuseBlock2Blocked = false;
    bool fuseBlock3Blocked = false;
    bool glovesOffWhenIsolation = false;
    public Light subdistributorLight;
    public List<bool> measureCounts;
    public GameObject technicalDrawingActive;
    public GameObject technicalDrawingFaded;
    bool coverPickable = false;
    public LayerMask maskDef;
    public LayerMask maskBlanket;
    public string telefon;
    bool checkDuspolSecond = false;
    private bool fuseBoxWithRedWhite=false;
    public Animator coverAnimator;
    bool coverOff;

    void Start()
    {
        maskDef = LayerMask.GetMask("Default");
        maskBlanket = LayerMask.GetMask("blanketActivator");
        manager.lineCheck.Add(isGreen);
        manager.lineCheck.Add(isGreen2);
        for (int i = 0; i < 10; i++)
        {
            measureCounts.Add(false);
        }
    }

    void Update()
    {
        if (showLight)
        {
            flashLight();
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance, maskDef))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.name == "SubSwitch")
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    //if(PickingUpScript.glovesOn)
                    //{
                    //    didSomeOfTheActionsWithGlove = true;
                    //}
                    phoneBeforeOtherStuff = false;
                    turnSubSwitch();
                }
            }
            else if (hit.transform.name == "firstCoverGrabArea")
            {
                manager.lineCheck[5] = true;
                Debug.Log("Cover is in range");
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    RemoveCover();
                }
                //    firstCover.AddComponent<Rigidbody>();
                //    firstCover.GetComponent<Rigidbody>().isKinematic = true;
                //    firstCover.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                //    activateFuses();
                //    firstCover.GetComponent<Rigidbody>().isKinematic = false;
                //    firstCover.GetComponent<Rigidbody>().useGravity = false;
                //    firstCover.transform.SetParent(handSpawnPoint.transform);
                //    firstCover.transform.localPosition = new Vector3(0, 0, 0.6f);
                //    firstCover.transform.localRotation = new Quaternion(0, 180f, 0, 0);
                //    firstCover.AddComponent<FixedJoint>();
                //    firstCover.GetComponent<FixedJoint>().connectedBody = handSpawnRg;
                //    inHand = true;
                //}
            }
            //else if (inHand && (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || dropTest))
            //{
            //    inHand = false;
            //    dropTest = false;
            //    firstCover.GetComponent<FixedJoint>().connectedBody = null;
            //    FixedJoint temp = firstCover.GetComponent<FixedJoint>();
            //    Destroy(temp);
            //    firstCover.GetComponent<Rigidbody>().useGravity = true;
            //    firstCover.transform.parent = null;
            //}


            else if (hit.transform.name == "phone")
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;

                    if (!phoneFirstPickUp && phoneBeforeOtherStuff)
                    {
                        Debug.Log("1");
                        correctPickUpPhone = true;
                        correctFirstPickUpPhone = true;
                        phoneFirstPickUp = true;
                        phoneStart.Play(1);
                        hit.transform.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
                        phoneScript.isGray = true;
                        telefon = "first";
                        return;
                    }
                    if (!phoneSecondPickUp && phoneBeforeIsolation)
                    {
                        if (telefon == "first")
                        {
                            phoneSecondPickUp = true;
                            phonePermit = true;
                            phonePermitSound.Play(1);
                            checkDuspolFirstTime = false;
                            hit.transform.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
                            phoneScript.isGray = true;
                            telefon = "second";
                            return;
                        }
                    }
                    if (!phoneThirdPickUp && phoneBeforeEnd)
                    {
                        Debug.Log("3");
                        phoneThirdPickUp = true;
                        phoneAtEnd = true;
                        phoneEnd.Play(1);
                        hit.transform.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
                        phoneScript.isGray = true;
                        telefon = "third";
                        return;
                    }
                    //if (!phonePlayChefOnce && phoneNotPickedUp)
                    //{
                    //    Debug.Log("4");
                    //    phoneRing.Stop();
                    //    phoneAngryChef.Play(1);
                    //    phonePlayedOnce = true;
                    //    phonePlayChefOnce = true;
                    //    phoneScript.gameObject.GetComponent<MeshRenderer>().materials[1].color = redLight.color;
                    //    phoneScript.startFlashing = false;
                    //    phoneScript.isGray = true;
                    //    return;
                    //}
                    switch (telefon)
                    {
                        case "first":
                            Debug.Log("1");
                            phoneStart.Play(1);
                            hit.transform.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
                            phoneScript.isGray = true;
                            break;
                        case "second":
                            Debug.Log("2");
                            phonePermitSound.Play(1);
                            hit.transform.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
                            phoneScript.isGray = true;
                            break;
                        case "third":
                            Debug.Log("3");
                            phoneEnd.Play(1);
                            hit.transform.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
                            phoneScript.isGray = true;
                            break;
                            //case "fourth":
                            //    Debug.Log("telefon 4");
                            //    phoneRing.Stop();
                            //    phoneAngryChef.Play(1);
                            //    phonePlayedOnce = true;
                            //    phonePlayChefOnce = true;
                            //    phoneScript.gameObject.GetComponent<MeshRenderer>().materials[1].color = redLight.color;
                            //    phoneScript.startFlashing = false;
                            //    phoneScript.isGray = true;
                            //    break;
                    }

                    //else if (!phoneNotPickedUp)
                    //{
                    //    correctPickUpPhone = true;
                    //    hit.transform.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
                    //    phoneScript.isGray = true;
                    //}
                }
            }
            else if (hit.transform.name == "firstSwitch")
            {

                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    //if (!correctPickUpPhone && !phonePlayedOnce && onceAngryChef)
                    //{
                    //    onceAngryChef = false;
                    //    phoneRing.Play(1);
                    //    phoneRing.loop = true;
                    //    phoneNotPickedUp = true;
                    //    phoneScript.startFlashing = true;
                    //}
                    turnFirstSwitch();
                }
            }
            //else if (hit.transform.parent == null)
            //{
            //    return;
            //}
            else if (hit.transform.parent.name == "fuses" && multitoolOn) // pri vtoriq
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    checkFuseMultitool(hit.transform.gameObject);
                }
            }
            else if (hit.transform.parent.name == "fuses" && duspolOn)
            {
                if ()
                {
                    manager.lineCheck[5] = true;
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;

                        phoneBeforeIsolation = true;
                        checkFuse(hit.transform.gameObject);
                    }
                }
            }
            else if (hit.transform.parent.name == "fuses" && gridsOn)
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;

                    SetGridsOnFusebox();
                    takeToolboxItem(redWhiteLabel);
                    takeGrids();
                }
            }

            else if (hit.transform.parent.name == "FuseBlocks" && fuseBlockerOn)
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;

                    takeOutFuse(hit.transform, true);
                }
            }
            else if (hit.transform.parent.name == "FuseBlocks" && fusePullerOn)
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    if (manager.headsetOn)
                    {
                        helmetWhenTakingFuses = true;
                    }

                    test = false;
                    takeOutFuse(hit.transform, false);
                }
            }
            else if (hit.transform.parent.name == "bolts" && screwOn)
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;

                    //if (!correctPickUpPhone && !phonePlayedOnce && onceAngryChef)
                    //{
                    //    onceAngryChef = false;
                    //    phoneRing.Play(1);
                    //    phoneRing.loop = true;
                    //    phoneNotPickedUp = true;
                    //    phoneScript.startFlashing = true;
                    //}
                    phoneBeforeOtherStuff = false;
                    hit.transform.gameObject.GetComponent<Animator>().enabled = true;
                    checkBolts();
                }
            }
            else if ((hit.transform.parent.name == "fuses" || hit.transform.name == "Fuse Box") && isolationOnFuseBox)
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    if (PickingUpScript.glovesOn == false) {
                        didSomeOfTheActionsWithGlove = true;
                    }
                    test = false;

                    phoneBeforeIsolation = true;
                    phoneBeforeEnd = false;

                    if (fuseBoxWithRedWhite)
                    {
                        fuseBoxRedWhiteLabel.SetActive(true);
                    }

                    isolationFuseBox.SetActive(false);
                    takeToolboxItem(isolation);
                    takeIsolation();
                    isolationOnFuseBox = false;
                }
            }
            else if ((hit.transform.parent.name == "fuses" || hit.transform.name == "Fuse Box") && isolationOn)
            {
                manager.lineCheck[5] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    if (isolationOnFuseBox == false)
                    {
                        if (!PickingUpScript.glovesOn)
                        {
                            glovesOffWhenIsolation = true;
                        }
                        phoneBeforeIsolation = false;
                        phoneBeforeEnd = true;

                        if (fuseBoxWithRedWhite)
                        {
                            fuseBoxRedWhiteLabel.SetActive(false);
                        }

                        isolationFuseBox.SetActive(true);
                        takeToolboxItem(isolation);
                        takeIsolation();
                        isolationOnFuseBox = true;
                    }
                }
            }
            else
            {
                manager.lineCheck[5] = false;
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit2, manager.distance, maskBlanket))
        {
            if (hit2.transform.name == "blanketActivator" && blanketOn)
            {
                manager.lineCheck[6] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    odeqlo.SetActive(true);

                    takeToolboxItem(blanket);
                    takeBlanket();
                    test = false;
                }
            }
            else if (hit2.transform.name == "blanketActivator" && odeqlo.activeSelf)
            {
                manager.lineCheck[6] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    odeqlo.SetActive(false);
                }
            }
            else
            {
                manager.lineCheck[6] = false;
            }
        }
        else
        {
            manager.lineCheck[6] = false;
            // manager.lineCheck[5] = false;
        }

    }

    public void SetGridsOnFusebox()
    {
        if (fuseBoxWithRedWhite==true)
        {
            fuseBoxWithRedWhite = false;
            fuseBoxRedWhiteLabel.SetActive(false);
        }
        else if(fuseBoxWithRedWhite == false)
        {
            fuseBoxWithRedWhite = true;
            fuseBoxRedWhiteLabel.SetActive(true);
        }
        
    }

    public void RemoveCover()
    {
        if (coverOff)
        {
            coverOff = false;
            deactivateFuses();
            coverAnimator.SetTrigger("returnCover");
        }
        else
        {
            coverOff = true;
            activateFuses();
            coverAnimator.SetTrigger("removeCover");
        }
    }

    int boltsOn = 4;
    public void checkBolts()
    {
        boltsOn--;
        if (boltsOn == 0)
        {
            boltsOff = true;
            coverHotspotArea.SetActive(true);

        }
        //foreach (GameObject bolt in bolts)
        //{
        //    if (bolt.activeSelf == true)
        //    {
        //        boltsOff = false;
        //        manager.lineCheck[5] = false;
        //        return;
        //    }
        //}
    }
    public void EndAssignment()
    {
        if (correctFirstPickUpPhone == false)
        {
            mistakesScript.addMistake("1");
        }
        if (subSwitchOn == false)
        {
            mistakesScript.addMistake("2");
        }
        if (lockAndSticker.activeSelf == false)
        {
            mistakesScript.addMistake("3");
        }
        if (onceFirstSwitchCheck == true || !notMeasuredAtAll)
        {
            mistakesScript.addMistake("4");
        }
        if (duspolTested == false)
        {
            mistakesScript.addMistake("5");
        }
        if (fuseBlock1 == true || fuseBlock2 == true || fuseBlock3 == true)
        {
            mistakesScript.addMistake("6");
        }
        if (helmetWhenTakingFuses==false)
        {
            mistakesScript.addMistake("7");
        }
        //if(didSomeOfTheActionsWithGlove == true)
        //{
        //    mistakesScript.addMistake("8");
        //}

        if (fuseBlock1Blocked == false || fuseBlock2Blocked == false || fuseBlock3Blocked == false)
        {
            mistakesScript.addMistake("8");
        }

        if (fuseBoxWithRedWhite == false)
        {
            mistakesScript.addMistake("9");
        }

        foreach (bool item in measureCounts)
        {
            if (item == false)
            {
                mistakesScript.addMistake("10");
                break;
            }
        }

        if (checkDuspolSecondTime == false)
        {
            mistakesScript.addMistake("11");
        }

        if (phonePermit == false)
        {
            mistakesScript.addMistake("12");
        }

        if (glovesOffWhenIsolation)
        {
            mistakesScript.addMistake("13");
        }

        if (isolationOnFuseBox == false)
        {
            bool temp13In=false;
           // mistakesScript.addMistake("14");  // see if can be put down
            foreach (string item in mistakesScript.mistakes)
            {
                if (item == "13") {
                    temp13In = true;
                }
            }
            if (!temp13In) {
                mistakesScript.addMistake("13");
            }
            mistakesScript.addMistake("14");
        }
        if (phoneAtEnd == false)
        {
            mistakesScript.addMistake("15");
        }
    }
    public void turnFirstSwitch()
    {
        if (firstSwitchOn == false)
        {
            firstSwitchOn = true;
            firstSwitch.transform.eulerAngles = new Vector3(0f, 90f, -90);
        }
        else if (firstSwitchOn == true)
        {
            firstSwitchOn = false;
            firstSwitch.transform.eulerAngles = new Vector3(-90f, 90f, -90f);
        }
    }
    public void turnSubSwitch()
    {
        if (subSwitchOn == false)
        {
            subSwitchOn = true;
            subdistributorLight.intensity = 0;
            subSwitch.transform.eulerAngles = new Vector3(180f, -90f, 90f);
        }
        else if (subSwitchOn == true)
        {

            if (padlockOn && subWithPadlock == false)
            {
                subWithPadlock = true;
                padlockOn = false;
                subSwitch.SetActive(false);
                padlockSubswitch.SetActive(true);
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
            else if (subWithPadlock && gridsOn)
            {
                onlyLock.SetActive(false);
                // here put the mistake

                lockAndSticker.SetActive(true);
                hand_redwhiteLabel.SetActive(false);
                glove_redwhiteLabel.SetActive(false);
                gridsOn = false;
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
            else if (subWithPadlock)
            {
                subWithPadlock = false;
                subSwitch.SetActive(true);
                padlockSubswitch.SetActive(false);
            }
            else
            {
                subdistributorLight.intensity = 1;
                subSwitchOn = false;
                subWithPadlock = false;
                padlockSubswitch.SetActive(false);
                onlyLock.SetActive(true);
                lockAndSticker.SetActive(false);
                subSwitch.SetActive(true);
                subSwitch.transform.eulerAngles = new Vector3(-90f, -90f, 90f);
            }
        }
    }
    public List<GameObject> stickersActive;
    public List<GameObject> stickersFaded;

    public GameObject onlyLock;
    public GameObject lockAndSticker;
    int counter = 0;
    bool onceFirstSwitchCheck=false;
    bool notMeasuredAtAll = false;
    void displayElectricity(string ifNotBlocked,string ifBlocked,int whichBlocked)
    {
        notMeasuredAtAll = true;
        counter++;
        if (counter == 12)
        {
            checkDuspolAgain = true;
        }
        if (onceFirstSwitchCheck == false && firstSwitchOn == false) {
            onceFirstSwitchCheck = true;
        }
        bool blockage = false;
        if (whichBlocked == 1)
        {
            blockage = fuseBlock1;
        }
        if (whichBlocked == 2)
        {
            blockage = fuseBlock2;
        }
        if (whichBlocked == 3)
        {
            blockage = fuseBlock3;
        }
        if (blockage == true)
        {
            
            foreach (TextMesh item in duspolDisplayAtFuse)
            {
                item.text = ifNotBlocked;
            }
            duspolText.text = ifNotBlocked;
            HandduspolText.text = ifNotBlocked;
            if (ifNotBlocked != "   0")
            {
                if (checkDuspolFirstTime)
                {
                    duspolTested = true;
                }
                if (checkDuspolAgain)
                {
                    checkDuspolSecondTime = true;
                }
            }
            foreach (bool item in measureCounts)
            {
                if (item == false)
                {
                    return;
                }
            }
            
        }
        else
        {
            foreach (TextMesh item in duspolDisplayAtFuse)
            {
                item.text = ifBlocked;
            }
            if (ifBlocked == "   0")
            {
                duspolText.text = "0";
                HandduspolText.text = "0";
            }
            else
            { 
                    if (checkDuspolFirstTime)
                    {
                        duspolTested = true;
                    }
                    if (checkDuspolAgain)
                    {
                        checkDuspolSecondTime = true;
                    }
                duspolText.text = ifBlocked;
                HandduspolText.text = ifBlocked;
            }
        }
    }
    string firstChecked = "";
    string secondChecked = "";
    public List<GameObject> redMultitools;
    public void checkFuseMultitool(GameObject currentFuse)
    {
        if (multitoolGlove.activeSelf == true) 
        {
            multitoolGlove.SetActive(false);
            multitoolGlove2.SetActive(true);
            foreach (GameObject item in multitoolAtFuses)
            {
                if (item.name == currentFuse.name)
                {
                    item.SetActive(true);
                }
            }
        }
        else if  (multitoolGlove2.activeSelf == true)
        {
            electricShock.Play();
            foreach (GameObject particle in electricParticles)
            {
                particle.SetActive(true); // particles effect when wrong move!
            }
            foreach (GameObject item in redMultitools)
            {
                if (item.name == "red" + currentFuse.name)
                {
                    item.SetActive(true);
                }
            }
            Invoke("hideMultitools",2f);
        }
        else if(hand_multimeter1.activeSelf == true)
        {
            hand_multimeter1.SetActive(false);
            hand_multimeter2.SetActive(true);
            foreach (GameObject item in multitoolAtFuses)
            {
                if (item.name == currentFuse.name)
                {
                    item.SetActive(true);
                }
            }
        }
        else if (hand_multimeter2.activeSelf == true)
        {
            electricShock.Play();
            foreach (GameObject particle in electricParticles)
           {
                particle.SetActive(true); // particles effect when wrong move!
            }
            foreach (GameObject item in redMultitools)
            {
                if (item.name == "red" + currentFuse.name)
                {
                    item.SetActive(true);
                }
            }

            Invoke("hideMultitoolsHand", 2f);
        }
    }

    public void hideMultitools()
    {
        foreach (GameObject item in multitoolAtFuses)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in redMultitools)
        {
            item.SetActive(false);
        }
        multitoolGlove.SetActive(true);
        multitoolGlove2.SetActive(false);
    }

    public void hideMultitoolsHand()
    {
        foreach (GameObject item in multitoolAtFuses)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in redMultitools)
        {
            item.SetActive(false);
        }
        hand_multimeter1.SetActive(true);
        hand_multimeter2.SetActive(false);
    }

    public void checkFuse(GameObject currentFuse)
    {
        if(hand_duspol1.activeSelf == true)
        {
            hand_duspol1.gameObject.SetActive(false);
            hand_duspol2.gameObject.SetActive(true);
            firstChecked = currentFuse.name;
            foreach (GameObject item in duspolAtFuses)
            {
                if (item.name == currentFuse.name)
                {
                    item.SetActive(true);
                }
            }
        }
        else if (hand_duspol2.activeSelf == true)
        {
            secondChecked = currentFuse.name;
            foreach (GameObject item in duspolAtFusesRight)
            {
                if (item.name == currentFuse.name)
                {
                    item.SetActive(true);
                }
            }
            checkCombinations(firstChecked, secondChecked);
            firstChecked = "";
            secondChecked = "";
        }
        if (duspolGlove1.activeSelf == true)
        {
            duspolGlove1.gameObject.SetActive(false);
            duspolGlove2.gameObject.SetActive(true);
            firstChecked = currentFuse.name;
            foreach (GameObject item in duspolAtFuses)
            {
                if (item.name == currentFuse.name)
                {
                    item.SetActive(true);
                }
            }
        }
        else if (duspolGlove2.activeSelf == true)
        {

            secondChecked = currentFuse.name;
            foreach (GameObject item in duspolAtFusesRight)
            {
                if (item.name == currentFuse.name)
                {
                    item.SetActive(true);
                }
            }
            checkCombinations(firstChecked, secondChecked);
            firstChecked = "";
            secondChecked = "";
        }
    }
    public bool checkCombinations(string firstFuse, string secondFuse)
    {
        if ((firstFuse == "1" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "1"))
        {
            displayElectricity("   0", "   0", 1);
            showLight = true;
            return true;
        } 
        if ((firstFuse == "1" && secondFuse == "5") || (firstFuse == "5" && secondFuse == "1"))
        {
            displayElectricity("400", "   0", 2);
            showLight = true;
            return true;
        } 
        if ((firstFuse == "1" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "1"))
        {
            displayElectricity("400", "   0", 3);
            showLight = true;
            return true;
        } 
        if (( firstFuse == "2" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "2" ))
        {

            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "400", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "1" ))
        {

            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "400", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "3"))
        {

            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "400", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "2"))
        {
            displayElectricity("400", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "5") || (firstFuse == "5" && secondFuse == "2"))
        {
            displayElectricity("   0", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "2"))
        {
            displayElectricity("400", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "3"))
        {
            displayElectricity("400", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "5") || (firstFuse == "5" && secondFuse == "3"))
        {
            displayElectricity("400", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "3"))
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "2") || (firstFuse == "2" && secondFuse == "1"))
        {
            displayElectricity("400", "400", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "3") || (firstFuse == "3" && secondFuse == "2"))
        {
            displayElectricity("400", "400", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "3") || (firstFuse == "3" && secondFuse == "1"))
        {
            displayElectricity("400", "400", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "1"))
        {
            displayElectricity("230", "230", 1);
            showLight = true;
            return true;
        } 
        if ((firstFuse == "4" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "4"))
        {
            displayElectricity("230", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "7" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "7"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 1);
            }
            else
            {
                displayElectricity("230", "   0", 1);
            }

            Debug.Log("first is " + firstFuse + "; second is " + secondFuse);
            duspolLight.GetComponent<MeshRenderer>().material = greenLight;
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "2"))
        {
            displayElectricity("230", "230", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "5" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "5"))
        {
            displayElectricity("230", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "8" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "8"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 2);
            }
            else
            {
                displayElectricity("230", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "3"))
        {
            displayElectricity("230", "230", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "6" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "6"))
        {
            displayElectricity("230", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "9" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "9"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("230", "   0", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "10" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "10"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("230", "230", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "1"))
        {
            displayElectricity("230", "230", 3);
            showLight = true;
            return true;
        }  // 11 e G/Y
        if ((firstFuse == "4" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "4"))
        {
            displayElectricity("230", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "7" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "7"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[6] = true;
            }
            else
            {
                displayElectricity("230", "   0", 1);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "2"))
        {
            displayElectricity("230", "230", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "5" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "5"))
        {
            displayElectricity("230", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "8" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "8"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[7] = true;
            }
            else
            {
                displayElectricity("230", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "3"))
        {
            displayElectricity("230", "230", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "6" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "6"))
        {
            displayElectricity("230", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "9" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "9"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[8] = true;
            }
            else
            {
                displayElectricity("230", "   0", 3);
            }
            showLight = true;
            return true;
        } 
        if ((firstFuse == "10" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "10"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[9] = true;
            }
            else
            {
                displayElectricity("230", "230", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "11" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "11")) //12 e blue
        {
                displayElectricity("   0", "   0", 3);           
                showLight = true;
                return true;
        }
        if ((firstFuse == "1" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "1"))
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "1"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 2);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "1"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 3);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "2"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock2 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 1);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "2"))
        {
            displayElectricity("   0", "   0", 3);                     
            showLight = true;
            return true;
        }
        if ((firstFuse == "2" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "2"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock2 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 3);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "3"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock3 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 1);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "3"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock3 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 2);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "3" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "3"))
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "4" && secondFuse == "5") || (firstFuse == "5" && secondFuse == "4"))
        {
            if (fuseBlock1 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 2);
            }      
        showLight = true;
        return true;
        }
        if ((firstFuse == "4" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "4"))
        {
            if (fuseBlock1 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }

            showLight = true;
            return true;
        }
        if ((firstFuse == "5" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "5"))
        {
            if (fuseBlock2 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }

            showLight = true;
            return true;
        }
        if ((firstFuse == "4" && secondFuse == "7") || (firstFuse == "4" && secondFuse == "7"))
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "4" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "4"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 2);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "4" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "4"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 3);
                }
            }
            showLight = true;
            return true;
        } 
        if ((firstFuse == "4" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "4"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {               
                displayElectricity("400", "   0", 1);
            }
            showLight = true;
            return true;
        } 
        if ((firstFuse == "5" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "5"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock2 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 1);
                }
            }
            showLight = true;
            return true;
        } 
        if ((firstFuse == "5" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "5"))
        {
            
                displayElectricity("   0", "   0", 3);
            
            showLight = true;
            return true;
        } 
        if ((firstFuse == "5" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "5"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock2 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 3);
                }
            }
            showLight = true;
            return true;
        } 
        if ((firstFuse == "5" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "5"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                    displayElectricity("400", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "6" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "6"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock3 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 1);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "6" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "6"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock3 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 2);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "6" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "6"))
        {
            
                displayElectricity("   0", "   0", 3);
           
            showLight = true;
            return true;
        }
        if ((firstFuse == "6" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "6"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }
            showLight = true;
            return true;
        } 
        if ((firstFuse == "7" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "7"))
        {           
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[0] = true;
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("400", "   0", 2);
                }
                else
                {
                    displayElectricity("   0", "   0", 2);
                    measureCounts[0] = true;
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "7" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "7"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[1] = true;
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("400", "   0", 3);            
                }
                else
                {
                    displayElectricity("   0", "   0", 3);
                    measureCounts[1] = true;
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "7" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "7"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[2] = true;
            }
            else
            {
                displayElectricity("400", "   0", 1);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "8" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "8"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[3] = true;
            }
            else
            {
                if (fuseBlock2 == false)
                {
                    displayElectricity("400", "   0", 3);
                }
                else
                {
                    displayElectricity("   0", "   0", 3);
                    measureCounts[3] = true;
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "8" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "8"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[4] = true;
            }
            else
            {
                displayElectricity("400", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "9" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "9"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
                measureCounts[5] = true;
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "1") || (firstFuse == "1" && secondFuse == "13"))
        {
            displayElectricity("0", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "2") || (firstFuse == "2" && secondFuse == "13"))
        {
            displayElectricity("400", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "3") || (firstFuse == "3" && secondFuse == "13"))
        {
            displayElectricity("400", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "13"))
        {
            displayElectricity("   0", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "5") || (firstFuse == "5" && secondFuse == "13"))
        {

            if (fuseBlock1 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "13"))
        {
            if (fuseBlock1 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "13"))
        {
          
                displayElectricity("   0", "   0", 3);
     
           
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "13"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 2);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "13"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock1 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 3);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "13"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 1);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "13"))
        {
            displayElectricity("230", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "13"))
        {
            displayElectricity("230", "   0", 1);
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "14") || (firstFuse == "14" && secondFuse == "13"))
        {
            if (fuseBlock1 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "13" && secondFuse == "15") || (firstFuse == "15" && secondFuse == "13"))
        {
            if (fuseBlock1 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "1") || (firstFuse == "1" && secondFuse == "14"))
        {
            displayElectricity("400", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "2") || (firstFuse == "2" && secondFuse == "14"))
        {
            displayElectricity("   0", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "3") || (firstFuse == "3" && secondFuse == "14"))
        {
            displayElectricity("400", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "14"))
        {
            if (fuseBlock2 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 1);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "5") || (firstFuse == "5" && secondFuse == "14"))
        {
            displayElectricity("   0", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "14"))
        {
            if (fuseBlock2 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "14"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock2 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 1);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "14"))
        {
       
                displayElectricity("   0", "   0", 3);
           
            showLight = true;
            return true;
            
        }
        if ((firstFuse == "14" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "14"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock2 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 3);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "14"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "14"))
        {
            displayElectricity("230", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "14"))
        {
            displayElectricity("230", "   0", 2);
            showLight = true;
            return true;
        }
        if ((firstFuse == "14" && secondFuse == "15") || (firstFuse == "15" && secondFuse == "14"))
        {
            if (fuseBlock2 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "1") || (firstFuse == "1" && secondFuse == "15"))
        {
            displayElectricity("400", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "2") || (firstFuse == "2" && secondFuse == "15"))
        {
            displayElectricity("400", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "3") || (firstFuse == "3" && secondFuse == "15"))
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "4") || (firstFuse == "4" && secondFuse == "15"))
        {
            if (fuseBlock3 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 1);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "5") || (firstFuse == "5" && secondFuse == "15"))
        {
            if (fuseBlock3 == false)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 2);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "6") || (firstFuse == "6" && secondFuse == "15"))
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "7") || (firstFuse == "7" && secondFuse == "15"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock3 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 1);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "8") || (firstFuse == "8" && secondFuse == "15"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                if (fuseBlock3 == false)
                {
                    displayElectricity("   0", "   0", 3);
                }
                else
                {
                    displayElectricity("400", "   0", 3);
                }
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "9") || (firstFuse == "9" && secondFuse == "15"))
        {
           
                displayElectricity("   0", "   0", 3);
           
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "10") || (firstFuse == "10" && secondFuse == "15"))
        {
            if (firstSwitchOn)
            {
                displayElectricity("   0", "   0", 3);
            }
            else
            {
                displayElectricity("400", "   0", 3);
            }
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "11") || (firstFuse == "11" && secondFuse == "15"))
        {
            displayElectricity("230", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "15" && secondFuse == "12") || (firstFuse == "12" && secondFuse == "15"))
        {   
            displayElectricity("230", "   0", 3);
            showLight = true;
            return true;
        }
        if ((firstFuse == "1" && secondFuse == "1") || (firstFuse == "2" && secondFuse == "2") || (firstFuse == "3" && secondFuse == "3"))
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if (firstFuse == "4" && secondFuse == "4")
        {
            displayElectricity("   0", "   0", 1);
            showLight = true;
            return true;
        }
        if (firstFuse == "5" && secondFuse == "5")
        {
            displayElectricity("   0", "   0", 2);
            showLight = true;
            return true;
        }
        if (firstFuse == "6" && secondFuse == "6")
        {
            displayElectricity("   0", "   0", 3);
            showLight = true;
            return true;
        }
        if (firstFuse == "7" && secondFuse == "7")
        {
           
                displayElectricity("   0", "   0", 3);   
          
            showLight = true;
            return true;
        }
        if (firstFuse == "8" && secondFuse == "8")
        {
          
                displayElectricity("   0", "   0", 3);
           
            showLight = true;
            return true;
        }
        if (firstFuse == "9" && secondFuse == "9")
        {
  
                displayElectricity("   0", "   0", 3);
          
            showLight = true;
            return true;
        }
        if (firstFuse == "10" && secondFuse == "10")
        {
         
                displayElectricity("   0", "   0", 3);
            
            showLight = true;
            return true;
        }
        if (firstFuse == "11" && secondFuse == "11")
        {
            displayElectricity("   0", "   0", 3);    
            showLight = true;
            return true;
        }
        if (firstFuse == "12" && secondFuse == "12")
        {

            displayElectricity( "  0", "  0", 3);            
            showLight = true;
            return true;
        }
        if (firstFuse == "13" && secondFuse == "13")
        {

            displayElectricity("  0", "  0", 1);
            showLight = true;
            return true;
        }
        if (firstFuse == "14" && secondFuse == "14")
        {

            displayElectricity("  0", "  0", 2);
            showLight = true;
            return true;
        }
        if (firstFuse == "15" && secondFuse == "15")
        {

            displayElectricity("  0", "  0", 3);
            showLight = true;
            return true;
        }

        displayElectricity("   0", "   0", 1);
        showLight = true;
        return false;
    }
    public void takeOutFuse(Transform fuse,bool redOrNo)
    {
        GameObject active = null;
        GameObject faded = null;
        GameObject red = null;
        foreach (Transform item in fuse)
        {
            if (item.gameObject.name == "Active")
            {
                active = item.gameObject;
            }
            else if (item.gameObject.name == "Faded")
            {
                faded = item.gameObject;
            }
            else if (item.gameObject.name == "red")
            {
                red = item.gameObject;
            }
        }
        if (redOrNo == false)
        {
            if (red.activeSelf)
            {
                if (fuse.name == "1")
                {
                    fuseBlock1Blocked = false;
                }
                if (fuse.name == "2")
                {
                    fuseBlock2Blocked = false;
                }
                if (fuse.name == "3")
                {
                    fuseBlock3Blocked = false;
                }
                red.SetActive(false);
                faded.SetActive(true);
            }
            else if (active.activeSelf)
            {
                if (fuse.name == "1")
                {
                    fuseBlock1 = false;
                }
                if (fuse.name == "2")
                {
                    fuseBlock2 = false;
                }
                if (fuse.name == "3")
                {
                    fuseBlock3 = false;
                }
                active.SetActive(false);
                faded.SetActive(true);
            }
            else if(faded.activeSelf)
            {
                if (fuse.name == "1")
                {
                    fuseBlock1 = true;
                }
                if (fuse.name == "2")
                {
                    fuseBlock2 = true;
                }
                if (fuse.name == "3")
                {
                    fuseBlock3 = true;
                }
                active.SetActive(true);
                faded.SetActive(false);
            }
        }
        else
        {
            if (faded.activeSelf)
            {
                if (fuse.name == "1")
                {
                    fuseBlock1Blocked = true;
                }
                if (fuse.name == "2")
                {
                    fuseBlock2Blocked = true;
                }
                if (fuse.name == "3")
                {
                    fuseBlock3Blocked = true;
                }
                red.SetActive(true);
                faded.SetActive(false);
            }
            else if (red.activeSelf)
            {

                if (fuse.name == "1")
                {
                    fuseBlock1Blocked = false;
                }
                if (fuse.name == "2")
                {
                    fuseBlock2Blocked = false;
                }
                if (fuse.name == "3")
                {
                    fuseBlock3Blocked = false;
                }
                red.SetActive(false);
                faded.SetActive(true);
            }
        }

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
                duspolLight.GetComponent<MeshRenderer>().material = standart;
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
                duspolLight.GetComponent<MeshRenderer>().material = standart;
                showLight = false;
                timer = 3;
                activateFuses();
            }
        }
    }
    public void pickUpItem()
    {
        foreach (GameObject item in multitoolAtFuses)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in redMultitools)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in duspolAtFusesRight)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in duspolAtFuses)
        {
            item.SetActive(false);
        }

        duspolOn = false;
            padlockOn = false;
            screwOn = false;
            fusePullerOn = false;
            fakeFuseOn = false;
        multitoolOn = false;
        blanketOn = false;
        isolationOn = false;
        fuseBlockerOn = false;
        technicalDrawingOn = false;
        clampsOn = false;
        gridsOn = false;
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
        else if(gridsOn)
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
 
    public void takeIsolation() // add without gloves
    {
        if (isolationOn==false)
        {
            pickUpItem();
            isolationOn = true;
            if (PickingUpScript.glovesOn)
            {
                isolationGlove.SetActive(true);
            }
            else
            {
                isolationHand.gameObject.SetActive(true);
            }

        }
        else if(isolationOn)
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
    public void takeTechnicalDrawing()
    {
        if(technicalDrawingOn == false)
        {
            pickUpItem();
            technicalDrawingOn = true;
            
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
    public void takeFakeFuse()
    {      
        if (fakeFuseOn == false)
        {
            pickUpItem();
            fakeFuseOn = true;
            fakeFuseGlove.SetActive(true);
        }
        else if (fakeFuseOn)
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
        if(fusePullerOn == false)
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
        else if(fusePullerOn)
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
            foreach (GameObject item2 in duspolAtFuses)
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

    public void activateFuses()
    {
        foreach (GameObject fuse in fuses)
        {
            fuse.SetActive(true);
        }
        foreach (Transform fuseBlocker in fuseBlockers)
        {
            fuseBlocker.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    public void deactivateFuses()
    {
        foreach (GameObject fuse in fuses)
        {
            fuse.SetActive(false);
        }
        foreach (Transform fuseBlocker in fuseBlockers)
        {
            fuseBlocker.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if ((other.gameObject.name == "firstCover"|| other.gameObject.name == "firstCover 1(Clone)") && boltsOff)
    //    {
    //        Debug.Log("Cover is in range");
    //        manager.lineCheck[6] = true;
    //        inRange = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.name == "firstCover" || other.gameObject.name == "firstCover 1(Clone)")
    //    {
    //        manager.lineCheck[6] = false;
    //        inRange = false;
    //    }
    //}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PickingUp2 : MonoBehaviour
{
    RaycastHit hit;
    public bool test = false;
    public GameManager2 manager;
    bool locked = false;
    GameObject active;
    GameObject faded;
    GameObject lastWrongItem;
  //  public Transform toolboardSub;
    public Transform toolboardMain;
    public FuseBoxScript2 fuseScript;
    public List<GameObject> activeFusePuller;
    public List<GameObject> fadedFusePuller;
    public GameObject bubbleGlove;
    public GameObject gloves;
    public bool glovesOn;
    public List<GameObject> technicalDrawingsActive;
    public List<GameObject> technicalDrawingsFaded;
    bool isGreen;
    public GameObject bubbleWithText;
    public TextMeshPro bubbleText;
    private void Start()
    {
        manager.lineCheck.Add(isGreen);
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance2))
        {
            if (hit.transform.parent.name == "Other")
            {
               
                if (locked)
                {
                    if (hit.transform.gameObject != lastWrongItem)
                    {
                        return;
                    }
                }
                if (lastWrongItem == null)
                {
                    CheckText(hit.transform.name);
                }
                manager.lineCheck[2] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    takeOtherItem(hit.transform);
                }
            }
            else if (hit.transform.name == "TechnicalDrawing" && !locked)
            {
                manager.lineCheck[2] = true;
                if ((OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !locked) || (test && !locked))
                {
                    test = false;
                    fuseScript.takeToolboxItem(hit.transform);
                    fuseScript.takeTechnicalDrawing();
                }
            }
            else if (hit.transform.parent.name == "PickUpObjects" && !locked)
            {
               
                CheckText(hit.transform.name);
                if (hit.transform.name == "TechnicalDrawing")
                {
                    return;
                }
                manager.lineCheck[2] = true;
              
              
                if ((OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && !locked) || (test && !locked))
                {
                    test = false;
                    
                    takeItem(hit.transform);
                }
            }
            else if (hit.transform.parent.name == "PickUpObjectsMain" && !locked)
            {
                manager.lineCheck[2] = true;
                CheckText(hit.transform.name);
                if (hit.transform.name == "TechnicalDrawing")
                {
                    return;
                }
                if (hit.transform.name == "Duspol")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeDuspol();
                    }
                }
                if (hit.transform.name == "Safety_Helmet")
                {
                    manager.lineCheck[2] = true;
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        manager.UseHeadset();
                    }
                }
                else if (hit.transform.name == "Gloves")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        Debug.Log("going in here?");
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        useGloves();
                    }
                }
                else if (hit.transform.name == "Screwdrivers")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeScrewdriver();
                    }
                }
                else if (hit.transform.name == "Blanket")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeBlanket();
                    }
                }
                else if (hit.transform.name == "Clamps")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        Debug.Log("taking clamp");
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeClamps();
                    }
                }
                else if (hit.transform.name == "SocketWrench")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeSocketWrench();
                    }
                }
                else if (hit.transform.name == "Grids")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeGrids();
                    }
                }
                else if (hit.transform.name == "Isolation" && glovesOn)
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeIsolation();
                    }
                }
                else if (hit.transform.name == "Padlocks")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takePadlock();
                    }
                }
                else if (hit.transform.name == "FusePuller")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeFusePuller();
                    }
                }
                else if (hit.transform.name == "DummyFuses" && (fuseScript.screwOn || fuseScript.dummyFuseOn))
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeDummyFuse();
                    }
                }
                else if (hit.transform.name == "DangerStickers")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeDangerStickers();
                    }
                }
                else if (hit.transform.name == "FuseBlocks" && fuseScript.fusePullerOn)
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeFuseBlocks();
                        foreach (GameObject item in activeFusePuller)
                        {
                            item.SetActive(false);
                        }
                        foreach (GameObject item in fadedFusePuller)
                        {
                            item.SetActive(true);
                        }
                    }
                }
                else if (hit.transform.name == "Multitool")
                {
                    if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                    {
                        test = false;
                        fuseScript.takeToolboxItem(hit.transform);
                        fuseScript.takeMultitool();
                    }
                }
            }
            else
            {
                bubbleWithText.SetActive(false);
                manager.lineCheck[2] = false;
            }

        }
        else
        {
            bubbleWithText.SetActive(false);
            manager.lineCheck[2] = false;
        }
    }

    public void CheckText(string name)
    {
        if (name == "Safety_Helmet")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Helm met gelaatsscherm incl. vlamboogprotectie";
        }
        else if (name == "Gloves")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Handschoenen";
        }
        else if (name == "Grids")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Label";
        }
        else if (name == "Screwdrivers")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "D-patroon Schroevendraaier";
        }
        else if (name == "FuseBlocks")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Meszekering dummies";
        }
        else if (name == "Padlocks")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Hangslot";
        }
        else if (name == "FusePuller")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Mespatroontrekker";
        }
        else if (name == "Duspol")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Duspol";
        }
        else if (name == "Isolation")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Beschermdoek";
        }
        else if (name == "Blanket")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Beschermmat";
        }
        else if (name == "DangerStickers")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Waarschuwingssticker";
        }
        else if (name == "Clamps")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Klemmen";
        }
        else if (name == "SocketWrench")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Dopsleutel";
        }
        else if (name == "DummyFuses")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "D-patroon 50A dummy’s";
        }
        else if (name == "Multitool")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Multimeter";
        }
        else if (name == "HelmetHalf")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Helm";
        }
        else if (name == "HelmetBlue")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Helm met gelaatsscherm";
        }
        else if (name == "ElectricScrewdriver")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Accuboormachine";
        }
        else if (name == "Glasses")
        {
            bubbleWithText.SetActive(true);
            bubbleText.text = "Veiligheidsbril";
        }

    }

    public void useGloves()
    {
        if (glovesOn == false)
        {
            glovesOn = true;
            //foreach (GameObject glovesActive in glovesActive)
            //{
            //    glovesActive.SetActive(false);
            //}

            //foreach (GameObject glovesFaded in glovesFaded)
            //{
            //    glovesFaded.SetActive(true);
            //}

            manager.Controller.gameObject.SetActive(false);
            gloves.gameObject.SetActive(true);
            manager.line.SetPosition(0, new Vector3(0f, 0, 0.11f));
            manager.line.SetPosition(1, new Vector3(0f, 0, 0.76f));
            fuseScript.pickUpItem();
        }
        else if (glovesOn == true)
        {
            glovesOn = false;
            //foreach (GameObject glovesActive in glovesActive)
            //{
            //    glovesActive.SetActive(true);
            //}
            //foreach (GameObject glovesFaded in glovesFaded)
            //{
            //    glovesFaded.SetActive(false);
            //}
            foreach (GameObject glove in fuseScript.allGloves)
            {
                glove.SetActive(false);
            }
            foreach (GameObject activeDrawings in technicalDrawingsActive)
            {
                activeDrawings.SetActive(true);
            }
            foreach (GameObject fadedDrawings in technicalDrawingsFaded)
            {
                fadedDrawings.SetActive(false);
            }
            fuseScript.pickUpItem();
            manager.Controller.gameObject.SetActive(true);
            manager.line.SetPosition(0, new Vector3(0, 0, 0));
            manager.line.SetPosition(1, new Vector3(0, 0, 0.76f));
        }
    }

    public void takeItem(Transform item)
    {
        foreach (Transform child in item)
        {
            if (child.name == "Active")
            {
                active = child.gameObject;
            }
            if (child.name == "Faded")
            {
                faded = child.gameObject;
            }

        }
        if (active.activeSelf)
        {
            active.SetActive(false);
            faded.SetActive(true);
            foreach (Transform item2 in toolboardMain)
            {
                if (item2.name == item.name)
                {
                    item2.gameObject.SetActive(true);
                }
            }
            //foreach (Transform item3 in toolboardSub)
            //{
            //    if (item3.name == item.name)
            //    {
            //        item3.gameObject.SetActive(true);
            //    }
            //}
        }
        else if (!active.activeSelf)
        {
            active.SetActive(true);
            faded.SetActive(false);
            foreach (Transform tool in fuseScript.allItems[0])
            {
                foreach (Transform to in tool)
                {
                    if (to.gameObject.name == "Faded")
                    {
                        if (to.gameObject.activeSelf == true)
                        {
                            if (tool.name == "Gloves")
                            {
                                fuseScript.takeToolboxItem(item);
                                useGloves();
                            }
                            else if (tool.name == "Screwdrivers")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeScrewdriver();
                            }
                            else if (tool.name == "Padlocks")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takePadlock();
                            }
                            else if (tool.name == "FuseBlocks")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeFuseBlocks();
                            }
                            else if (tool.name == "FusePuller")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeFusePuller();
                            }
                            else if (tool.name == "Duspol")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeDuspol();
                            }
                            else if (tool.name == "Multitool")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeMultitool();
                            }
                            else if (tool.name == "Isolation")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeIsolation();
                            }
                            else if (tool.name == "Blanket")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeBlanket();
                            }
                            else if (tool.name == "Clamps")
                            {
                                fuseScript.takeToolboxItem(item);
                                fuseScript.takeClamps();
                            }
                        }
                    }
                }
            }

            foreach (Transform item2 in toolboardMain)
            {
                if (item2.name == item.name)
                {
                    item2.gameObject.SetActive(false);
                }
            }
            //foreach (Transform item3 in toolboardSub)
            //{
            //    if (item3.name == item.name)
            //    {
            //        item3.gameObject.SetActive(false);
            //    }
            //}
        }
    }

    public void takeOtherItem(Transform item)
    {

        GameObject active = null;
        GameObject faded = null;
        bubbleWithText.SetActive(false);
        foreach (Transform child in item)
        {
            if (child.name == "Active")
            {
                active = child.gameObject;
            }
            if (child.name == "Faded")
            {
                faded = child.gameObject;
            }
        }
        if (active.activeSelf == true)
        {
            Debug.Log("getting a wrong item");
            locked = true;
            lastWrongItem = hit.transform.gameObject;
            bubbleGlove.SetActive(true);
            active.SetActive(false);
            faded.SetActive(true);
        }
        else if (active.activeSelf == false)
        {
            locked = false;
            lastWrongItem = null;
            bubbleGlove.SetActive(false);
            active.SetActive(true);
            faded.SetActive(false);

        }
    }
}

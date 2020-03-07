using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class socketsScript : MonoBehaviour
{
    RaycastHit hit;
    public GameManager2 manager;
    bool isGreen = false;
    public bool test;
    public GameObject hand_duspol1;
    public GameObject hand_duspol2;
    public GameObject duspolGlove1;
    public GameObject duspolGlove2;
    public List<GameObject> rightDuspols;
    public List<GameObject> leftDuspols;
    public bool timerStart = false;
    public List<TextMesh> dupolDisplay;
    GameObject activeDuspol1;
    GameObject activeDuspol2;
    float timer = 3f;
    private void Start()
    {
        manager.lineCheck.Add(isGreen);
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 6f))
        {
            Debug.Log(hit.transform.parent.name);
            if (hit.transform.parent.name == "Sockets" && gameObject.GetComponent<FuseBoxScript2>().duspolOn)
            {
                manager.lineCheck[0] = true;
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
                {
                    test = false;
                    checkSocket(hit.transform);
                }
            }
            else {
                manager.lineCheck[0] = false;
            }
        }
        else
        {
            manager.lineCheck[0] = false;
        }
        if (timerStart) {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("timer is 0 turning off");
                timerStart = false;
                timer = 3;
                foreach (TextMesh item in dupolDisplay)
                {
                    item.text ="0";
                }
                activeDuspol1.gameObject.SetActive(false);
                activeDuspol2.gameObject.SetActive(false);
                if (hand_duspol2.activeSelf == true)
                {
                    hand_duspol1.gameObject.SetActive(true);
                    hand_duspol2.gameObject.SetActive(false);
                }
                else if (duspolGlove2.activeSelf == true) {
                    duspolGlove1.gameObject.SetActive(true);
                    duspolGlove2.gameObject.SetActive(false);
                }
            }
        }
    }

    public void checkSocket(Transform currentFuse)
    {
        if (gameObject.GetComponent<FuseBoxScript2>().checkDuspolFirstTime == false && gameObject.GetComponent<FuseBoxScript2>().duspolTested==false)
        {
            gameObject.GetComponent<FuseBoxScript2>().duspolTested = true;
        }

        if (hand_duspol1.activeSelf == true)
        {
            Debug.Log("FIRST HAND");
            hand_duspol1.gameObject.SetActive(false);
            hand_duspol2.gameObject.SetActive(true);
            foreach (GameObject item in leftDuspols)
            {
                if (item.name == "left_"+currentFuse.name)
                {
                    item.SetActive(true);
                    activeDuspol1 = item;
                }
            }
        }
        else if (hand_duspol2.activeSelf == true)
        {
            foreach (GameObject item in rightDuspols)
            {
                if (item.name == "right_" + currentFuse.name)
                {
                    item.SetActive(true);
                    activeDuspol2 = item;
                }
            }
            Debug.Log("SECOND HAND");
            foreach (TextMesh item in dupolDisplay)
            {
                item.text = "230";
            }
            timerStart = true;
            if (gameObject.GetComponent<FuseBoxScript2>().measurementsCorrect)
            {
                gameObject.GetComponent<FuseBoxScript2>().checkDuspolSecondTime = true;
            }
        }
        else if (duspolGlove1.activeSelf == true)
        {
            duspolGlove1.gameObject.SetActive(false);
            duspolGlove2.gameObject.SetActive(true);
            foreach (GameObject item in leftDuspols)
            {
                if (item.name == "left_"+currentFuse.name)
                {
                    item.SetActive(true);
                    activeDuspol1 = item;
                }
            }
        }
        else if (duspolGlove2.activeSelf == true)
        {
            foreach (GameObject item in rightDuspols)
            {
                if (item.name == "right_"+currentFuse.name)
                {
                    item.SetActive(true);
                    activeDuspol2 = item;
                }
            }
            foreach (TextMesh item in dupolDisplay)
            {
                item.text = "230";
            }
            timerStart = true;
            if (gameObject.GetComponent<FuseBoxScript2>().measurementsCorrect)
            {
                gameObject.GetComponent<FuseBoxScript2>().checkDuspolSecondTime = true;
            }
        }
    }
}

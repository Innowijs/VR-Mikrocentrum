using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAtScript : MonoBehaviour
{
    float timer=4f;
    bool looking = false;
    public FuseBoxScript2 fuseScript;
    RaycastHit hit;
    public GameManager2 manager;

    public void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, manager.distance2))
        {
            if (hit.transform.name == "MainBlueBox")
            {
                Debug.Log("looking at map!! " + timer);
                looking = true;
            }
            else {
                looking = false;
                timer = 4f;
            }
        }
        else
        {
            looking = false;
            timer = 4f;
        }
        if (looking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (fuseScript.canLookAtMap)
                {
                    Debug.Log("looking done right");
                    fuseScript.mapLooked4seconds = true;
                }
            }
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.name == "MainBlueBox")
    //    {
    //        Debug.Log("looking at map!!");
    //        looking = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.name == "MainBlueBox")
    //    {
    //        Debug.Log("STOP LOOKING AT THE MAP !!");
    //        timer = 4f;
    //        looking = false;
    //    }

    //}
}

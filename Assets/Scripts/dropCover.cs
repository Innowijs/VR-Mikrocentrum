using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropCover : MonoBehaviour {
    public AudioSource dropSound;
    public GameObject fuseScript;
    public GameObject newCoverPrefab;
    public Transform fuseBoxParent;
    public bool canReturn=false;
    GameObject newCover;
    private void Start()
    {
        fuseBoxParent = GameObject.Find("Fuse Box").GetComponent<Transform>();
        fuseScript = GameObject.Find("OculusGoControllerModel");
    }
    IEnumerator removeRB()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(3f);
        Rigidbody temp = gameObject.GetComponent<Rigidbody>();
        Destroy(temp);
        Debug.Log("rb destroyed");
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "room3Floor")
        {
            dropSound.Play();
            StartCoroutine(removeRB());
        }
        if(collision.gameObject.name == "coverHotspot")
        {
            Debug.Log("cover hotspot reached ");
            
            if (transform.parent == null)
            {
                gameObject.GetComponent<Transform>().localPosition = new Vector3(23, 1.3f, -22.9f);
                gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(-13, 0, 0);
            }
            else if(transform.parent.gameObject.name == "handSpawnPoint")
            {
                transform.parent = null;
                gameObject.GetComponent<Transform>().localPosition = new Vector3(23, 1.3f, -22.9f);
                gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(-13, 0, 0);
            }
            FixedJoint temp = gameObject.GetComponent<FixedJoint>();
            Destroy(temp);
            Rigidbody temp2 = gameObject.GetComponent<Rigidbody>();
            Destroy(temp2);
        }
        if (collision.gameObject.name == "Fuse Box" && canReturn)
        {
            Debug.Log("vlizam i izgasqm");
            canReturn = false;
            Destroy(gameObject);
            GameObject newCover =  Instantiate(newCoverPrefab, fuseBoxParent);
            newCover.GetComponent<dropCover>().newCoverPrefab = newCoverPrefab;
            foreach (Transform item in newCover.transform)
            {
                Debug.Log("looking");
                if(item.name == "firstSwitch")
                {
                    Debug.Log("found first switch");
                    fuseScript.GetComponent<FuseBoxScript>().firstSwitch = item.gameObject;
                }
                //if(item.name == "bolts")
                //{
                //    Debug.Log("found bolts");
                //    fuseScript.GetComponent<FuseBoxScript>().bolts = new List<GameObject>();
                //    foreach (Transform bolt in item)
                //    {
                //        fuseScript.GetComponent<FuseBoxScript>().bolts.Add(bolt.gameObject);
                //    }
                //}
            }
            if(fuseScript.GetComponent<FuseBoxScript>().firstSwitchOn== true) //fause default
            {
                fuseScript.GetComponent<FuseBoxScript>().firstSwitchOn = false;
                fuseScript.GetComponent<FuseBoxScript>().turnFirstSwitch();
            }
            else
            {
                fuseScript.GetComponent<FuseBoxScript>().firstSwitchOn = false;
            }
            fuseScript.GetComponent<FuseBoxScript>().deactivateFuses();
            //fuseScript.GetComponent<FuseBoxScript>().boltsOff = false;        // trqbva da gi ostavq true
            fuseScript.GetComponent<FuseBoxScript>().firstCover = newCover;
            fuseScript.GetComponent<FuseBoxScript>().manager.lineCheck[6] = false;
            fuseScript.GetComponent<FuseBoxScript>().inRange = false;
            Debug.Log("krai na smqnata");
        }
    }
    private void OnTriggerExit(Collider collision)
    {
       
        if (collision.gameObject.name == "Fuse Box")
        {
            Debug.Log("izlizam i chakam");
            canReturn = true;
        }
    }
}

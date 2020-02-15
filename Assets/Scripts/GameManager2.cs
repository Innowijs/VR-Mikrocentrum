using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public LineRenderer line;
    public float distance2 = 4f;
    public Material lineRed;
    public Material lineGreen;
    public List<bool> lineCheck;
    public GameObject cameraRig;
    bool headsetOn;
    public List<Light> lightInTheRoom;
    public List<GameObject> helmetActive;
    public List<GameObject> helmetFaded;
    public GameObject Controller;
    void Start()
    {
        cameraRig.GetComponent<Transform>().position = new Vector3(cameraRig.GetComponent<Transform>().position.x, 2.1f, cameraRig.GetComponent<Transform>().position.z);
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
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.DpadDown))
        {
            cameraRig.GetComponent<Transform>().position = new Vector3(cameraRig.GetComponent<Transform>().position.x, 1.4f, cameraRig.GetComponent<Transform>().position.z);
        }
        if (OVRInput.GetDown(OVRInput.Button.DpadUp))
        {
            cameraRig.GetComponent<Transform>().position = new Vector3(cameraRig.GetComponent<Transform>().position.x, 2.1f, cameraRig.GetComponent<Transform>().position.z);
        }
    }
    public void UseHeadset()
    {
        if (headsetOn == true)
        {
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
}

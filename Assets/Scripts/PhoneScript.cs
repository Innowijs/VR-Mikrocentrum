using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScript : MonoBehaviour {

    public Material redMat;
    public Material greyMat;
    public Material greenLight;
    public bool startFlashing = false;
    float timer=0.5f;
    float timerGray = 4f;
    bool red = true;
    public bool isGray = false;
    public List<GameObject> phones;
	void Update () {
        if (startFlashing)
        {
            startFlickering();
        }
        if (isGray)
        {
            makeGray();
        }
	}
    private void startFlickering()
    {
        timer -= Time.deltaTime;
        if(timer<= 0)
        {
            if (red)
            {
                red = false;
                timer = 0.5f;
                foreach (GameObject item in phones)
                {
                    item.GetComponent<MeshRenderer>().materials[1].color = redMat.color;
                }
            }
            else
            {
                red = true;
                timer = 0.5f;
                foreach (GameObject item in phones)
                {
                  item.GetComponent<MeshRenderer>().materials[1].color = greyMat.color;
                }
            }
        }
    }

    public void ActivetePhone(Transform phone)
    {
        phone.GetComponent<MeshRenderer>().materials[1].color = greenLight.color;
        isGray = true;
    }

    public void makeGray()
    {
        timerGray -= Time.deltaTime;
        if(timerGray <= 0)
        {
            Debug.Log("inside");
            foreach (GameObject item in phones)
            {
              item.GetComponent<MeshRenderer>().materials[1].color = greyMat.color;
            }
            
            isGray = false;
            timerGray = 4f;
        }
        
    }
}

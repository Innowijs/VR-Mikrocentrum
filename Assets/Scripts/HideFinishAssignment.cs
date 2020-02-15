using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFinishAssignment : MonoBehaviour {

    public List<GameObject> papers;
    public GameObject gameManager;
    public bool inRange = false;
    bool greenLine = false;
    public bool test = false;

    private void Update()
    {
        if (inRange)
        {
            gameManager.GetComponent<GameManager>().lineCheck[8] = true;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || test)
            {
                test = false;
                inRange = false;
                foreach (GameObject paper in papers)
                {
                    paper.SetActive(false);
                }
                gameManager.GetComponent<GameManager>().lineCheck.RemoveAt(8);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OculusGoControllerModel")
        {
            inRange = true;
            gameManager.GetComponent<GameManager>().lineCheck.Add(greenLine);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "OculusGoControllerModel")
        {
            inRange = false;
            gameManager.GetComponent<GameManager>().lineCheck.RemoveAt(8);
        }
    }
}

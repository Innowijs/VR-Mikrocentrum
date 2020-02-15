using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayerTable : MonoBehaviour {

    public GameManager gameManagerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OVRPlayerController")
        {
            gameManagerScript.FirstHalfTutorial();
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

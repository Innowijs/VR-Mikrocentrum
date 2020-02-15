using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayer : MonoBehaviour {
    public GameManager gameManagerScript;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "OVRPlayerController")
        {
            if (gameManagerScript.fourthTutorialStart)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameManagerScript.FourthTutorial();
            }
            
        }
    }
}

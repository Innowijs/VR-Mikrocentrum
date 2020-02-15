using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPlayerMap : MonoBehaviour {

    public GameManager gameManagerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "OVRPlayerController")
        {
            if (gameManagerScript.nineTutorialStart)
            {
                gameManagerScript.nineTutorial();
            }
        }
    }
}

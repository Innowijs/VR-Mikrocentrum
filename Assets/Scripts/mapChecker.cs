using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapChecker : MonoBehaviour {

    public bool doorChecker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DoorChecker")
        {
            doorChecker = true;
            Debug.Log("Can NOT");
        }
    }
    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.name == "DoorChecker")
         {
            doorChecker = false;
            Debug.Log("Can be grabbed");
         }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public List<TextMesh> clocks;
    int minuteCounter = 14;
    int hourCounter = 08;
    string minutes;
    string hours;
    public FuseBoxScript2 fuseboxScript;
    void Start()
    {
     StartCoroutine("moveTime");
    }

    public IEnumerator moveTime() {
        minuteCounter++;
        if (minuteCounter == 60)
        {
            minuteCounter = 0;
            hourCounter++;
            if (hourCounter > 24)
            {
                hourCounter = 0;
            }
        }     
        minutes = string.Format("{0:00}", minuteCounter);
        hours = string.Format("{0:00}", hourCounter);
        foreach (TextMesh item in clocks)
        {
            item.text = hours + ":" + minutes;
        }
        yield return new WaitForSecondsRealtime(10f);
        StartCoroutine("moveTime");
    }
    public IEnumerator StartCountdown()
    {
        Debug.Log("CLOCK started");
        yield return new WaitForSecondsRealtime(10f * 14f);
        Debug.Log("CLOCK IS READY!!!!!!!!!!!!!!!!!!!!");
        fuseboxScript.ClockReady = true;
    }
}

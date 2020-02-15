using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongMaterialBubble : MonoBehaviour {

    float timer = 4f;
    bool flash = false;
    public GameObject bubble;

	void Update () {
        if (flash)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                flash = true;
                timer = 4;
                bubble.SetActive(false);
            }
        }
	}
    public void activate()
    {
        timer = 4;
        bubble.SetActive(true);
        flash = true;
    }
}

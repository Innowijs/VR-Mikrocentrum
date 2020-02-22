using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class splashScreen : MonoBehaviour
{
    float timer = 5;
    bool start =false;

    AsyncOperation asyncOperation;
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0)
            {
                    asyncOperation.allowSceneActivation = true;
            }
        }
    }

    IEnumerator LoadScene()
    {
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(1); 
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        while (!asyncOperation.isDone)
        {
         Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            if (asyncOperation.progress >= 0.9f)
            {
                start = true;
                Debug.Log("Press the space bar to continue");               
            }
            yield return null;
        }
    }
}

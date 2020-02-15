using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverReact : MonoBehaviour
{
    public RuntimeAnimatorController returnCover;
    public RuntimeAnimatorController removeCover;

    public void ChangeAnimatorReturn()
    {
        Debug.Log("111111111111111");
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = removeCover;
        gameObject.transform.localPosition = new Vector3(0.0006f, -0.052f, -0.023f);
    }

    public void ChangeAnimatorRemove()
    {
        Debug.Log("22222222222222222");
 
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = returnCover;
        gameObject.transform.localPosition = new Vector3(0.0006f, -0.4f, 0.1f);
    }
}

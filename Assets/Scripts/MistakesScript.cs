using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistakesScript : MonoBehaviour {
    public List<string> mistakes;
    public void addMistake(string mistakeNumber)
    {
        mistakes.Add(mistakeNumber);
    }
}

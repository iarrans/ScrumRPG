using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    public void Play()
    {
        GameObject.FindGameObjectWithTag("TutorialButton").GetComponent<Button>().enabled = false;
    }
}

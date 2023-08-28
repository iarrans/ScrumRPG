using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UvusInput : MonoBehaviour
{
    public Text uvusText;

    public RestDBAPI statsApi;

    public void UpdateUvus()
    {
        string newUvus = uvusText.text;
        statsApi.uvus = newUvus;
        Debug.Log("New uvus: " + newUvus);
    }
}

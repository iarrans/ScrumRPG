using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnsRemaining : MonoBehaviour
{
    Project project;

    // Start is called before the first frame update
    void Start()
    {
        project = GameObject.FindGameObjectWithTag("Project").GetComponent<Project>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = project.GetRemainingTurns().ToString();
    }
}

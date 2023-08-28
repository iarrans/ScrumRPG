using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintStartButtonBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List<List<int>> sprintBacklog = TaskListLists.sprintBacklogTasks;
        bool b = sprintBacklog.Count > 0;
        GetComponent<Button>().interactable = b;
        Color texColor = transform.GetChild(0).GetComponent<Text>().color;
        if (b) transform.GetChild(0).GetComponent<Text>().color = new Color(texColor.r, texColor.g, texColor.b, 1);
        else transform.GetChild(0).GetComponent<Text>().color = new Color(texColor.r, texColor.g, texColor.b, 0.5f);
    }
}

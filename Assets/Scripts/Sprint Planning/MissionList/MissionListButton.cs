using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionListButton : MonoBehaviour
{
    MissionsEvaluations missionsEvaluations;
    // Start is called before the first frame update
    void Start()
    {
        missionsEvaluations = GameObject.FindGameObjectWithTag("Project").GetComponent<MissionsEvaluations>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Mission> missions = missionsEvaluations.missions;
        GetComponent<Button>().interactable = missions.Count > 0;
    }
}

using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlanning : MonoBehaviour
{
    Canvas CanvasObject;
    public TaskListLists taskController;

    public Sprint sprintEvent;

    public void EndSprintPlanning()
    {
        ORK.Control.EnablePlayerControls(true);
        CanvasObject = GameObject.Find("UI Sprint Planning").GetComponent<Canvas>();
        CanvasObject.GetComponent<Canvas>().enabled = false;
        Group sprintBacklog = taskController.CreateCombatantGroup();
        List<List<int>> sprintBacklogList = taskController.GetSprintBacklogList();
        GameObject.FindGameObjectWithTag("Project").GetComponent<Project>().sprintBacklogExperience = sprintEvent.GetTotalStoryPoints();
        //A continuación, se asignaría el grupo de combatientes como el del sprint

        //Transportamos el grupo al evento de combate
        GameObject[] party = GameObject.FindGameObjectsWithTag("GroupCombatant");
        foreach (GameObject member in party)
        {
            member.transform.position = new Vector3(25, 20, 0);
        }

        Debug.Log("BattleSize: " + sprintBacklog.BattleSize);
    }
}

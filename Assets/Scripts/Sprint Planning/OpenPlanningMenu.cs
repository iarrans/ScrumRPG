using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class OpenPlanningMenu : MonoBehaviour 
{
    private Canvas CanvasObject;

    private void Start()
    {
        CanvasObject = GameObject.Find("UI Sprint Planning").GetComponent<Canvas>();
        CanvasObject.GetComponent<Canvas>().enabled = false;
    }

    public void OpenSprintPlanningMenu()
    {
        //Bucle para mostrar la interfaz de los personajes
        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
        foreach (GameObject member in grupoCombatants)
        {
            string superrol = member.GetComponent<Superrol>().superrol;
            int rol = 0;
            switch (superrol)
            {
                case "Developer":
                    rol = 0;
                    break;
                case "Quality Assurance":
                    rol = 1;
                    break;
                case "Tester":
                    rol = 2;
                    break;
                case "UI Designer":
                    rol = 3;
                    break;
                case "Deployment":
                    rol = 4;
                    break;
            }
            ComponentHelper.GetCombatant(member).Class.Change(rol, false, true, true, false, false, false, false, false);
        }
        GameObject planning =  GameObject.Find("Sprint Planning Scripts");
        planning.GetComponent<TaskListLists>().EmptySB();
        CanvasObject.GetComponent<Canvas>().enabled = true;
        MainMenu.DisplayPartyUI();
    }
}

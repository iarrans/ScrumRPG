using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSelector : MonoBehaviour
{
    public GameObject selector;

    public Text textoTurnos;

    [HideInInspector]
    public Sprint sprintEvent;
    TaskListLists sprintPlanningScripts;
    Project project;

    int n = 1;

    private void Start()
    {
        project = GameObject.FindGameObjectWithTag("Project").GetComponent<Project>();
        sprintEvent = GameObject.FindGameObjectWithTag("BattleMain").GetComponent<Sprint>();
        sprintPlanningScripts = GameObject.FindGameObjectWithTag("SprintPlanningScripts").GetComponent<TaskListLists>();
    }

    public void Update()
    {
        if (sprintPlanningScripts.loaded)
        {
            if (n <= 0)
            {
                sprintEvent = GameObject.FindGameObjectWithTag("BattleMain").GetComponent<Sprint>();
                float sliderValue = selector.GetComponent<Slider>().value;

                int numTurnos = ParseSliderValueToTurnNumber(sliderValue);

                UpdateSlider(numTurnos);
            }
            else
            {
                n -= 1;
            }
        }
    }

    void UpdateSlider(int numTurnos)
    {
        int remainingTurns = project.GetRemainingTurns();
        if (numTurnos > remainingTurns)
        {
            numTurnos = remainingTurns;
            selector.GetComponent<Slider>().value = (numTurnos + 1) / 40f;
        }

        textoTurnos.text = "Number of turns: " + numTurnos.ToString();
        sprintEvent.SetTurns(numTurnos);

    }

    int ParseSliderValueToTurnNumber(float sliderValue)
    {
        int numTurnos = Mathf.FloorToInt(39 * sliderValue) + 1;

        return numTurnos;
    }

}

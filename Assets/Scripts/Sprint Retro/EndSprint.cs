using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSprint : MonoBehaviour
{
    public GameObject project;

    public GameObject EndSprintCanvas;

    public GameObject EndSprintUI;

    public Text resultText;

    public Image resultImage;

    public Image bg;

    public Sprite victory;

    public Sprite defeat;

    //Textos sobre las etsadísticas

    public Text sprintNumber;
    public Text pO;
    public Text motivation;
    public Text phmedia;
    public Text phratio;
    public Text duracionMedia;
    public Text duracionratio;

    private void Start()
    {
        EndSprintCanvas.GetComponent<Canvas>().enabled = false;
    }


    public void BeginSprint()
    {
        Project proyecto = project.GetComponent<Project>();
        if (proyecto.productBacklog.Count > 0 && proyecto.GetRemainingTurns()> 0)
        {
            Debug.Log("Project not ended. Starting new sprint");
            project.GetComponent<OpenPlanningMenu>().OpenSprintPlanningMenu();
            ORKFramework.ORK.GlobalEvents.Get(8).Call(); //Poner aquí como global event spawnear a la gente
            ORKFramework.ORK.GlobalEvents.Get(6).Call();
        } 
        else if (proyecto.productBacklog.Count > 0)
        {
            //activamos el canvas
            EndSprintCanvas.GetComponent<Canvas>().enabled = true;
            //activamos el panel
            EndSprintUI.SetActive(true);
            //Seteamos los atributos
            resultText.text = "Defeat: project run out of turns";
            resultImage.sprite = defeat;
            bg.color = new Color(1, (float)0.2877, (float)0.2877);

            generateFinalStats();

        }
        else
        {
            //activamos el canvas
            EndSprintCanvas.GetComponent<Canvas>().enabled = true;
            //activamos el panel
            EndSprintUI.SetActive(true);
            //Seteamos los atributos
            resultText.text = "Victory: project backlog finished";
            resultImage.sprite = victory;
            bg.color = new Color((float)0.6948, (float)0.9811, (float)0.6340);

            generateFinalStats();

        }

    }

    public void generateFinalStats()
    {
        SprintStatistics stats = project.GetComponent<SprintStatistics>();

        int numSprints = stats.phPercentage.Count;

        List<float> ratios = stats.FinalStatistics();

        sprintNumber.text = "During the proyect, " + numSprints + " sprints were planned!";
        pO.text = ratios[0] + "% of the Sprints were succesful";
        motivation.text = "Average team motivation at their endings was " + ratios[1];
        phmedia.text = "Average story points defeated per sprint was " + ratios[2];
        phratio.text = "On average, " + ratios[3] + "% story points were completed each sprint";
        duracionMedia.text = "Average sprint duration was " + ratios[4] + " turns";
        duracionratio.text = "On average, " + ratios[5] + "% of the time was spent of each sprint";

        RestDBAPI apiCall = project.GetComponent<RestDBAPI>(); //Llamamos a la API para hacer el post a la base de datos de las estadísticas obtenidas
        apiCall.SendStatistics(numSprints, ratios[0], ratios[1], ratios[2], ratios[3], ratios[4], ratios[5]);
    }

}

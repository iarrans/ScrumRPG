using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetroMissionButton : MonoBehaviour
{
    public Text missionName;

    public Text missionDescripction;

    public Text motivation;

    //Atributos para poder distinguir qué tipo de misión es

    //public Text missionIndex;
    public int missionIndex;

    //Controlador
    public SprintRetro buttonController;

    public void AddMission()
    {
        Debug.Log("We're going to add a time mission in position " + missionIndex + " of the list");
        buttonController.AddMission(missionIndex);
    }

}

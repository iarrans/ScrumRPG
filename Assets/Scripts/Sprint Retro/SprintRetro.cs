using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintRetro : MonoBehaviour
{

    public GameObject project;

    //listas de misiones que finalmente se escogerán para el sprint
    public MissionsEvaluations missionsLists;

    //IU del Sprint Retrospective
    public GameObject SprintRetroUICanvas;

    //Panel con toda la UI del sprint retrospective
    public GameObject PanelUI;

    public List<GameObject> presentMissionButtons;

    public GameObject buttonPrefab;

    public Text missionschosentext;

    public Scrollbar scroll;


    void Start()
    {
        SprintRetroUICanvas.GetComponent<Canvas>().enabled = false;

    }

    public void StartRetrospective()
    {
        scroll.value = 0;

        SprintRetroUICanvas.GetComponent<Canvas>().enabled = true;
        //activamos el panel
        PanelUI.SetActive(true);

        missionsLists.EmptyLists();
        //rellenar las listas de misiones de retro
        missionsLists.PopulateMissionOptions();
        //spawnear botones, borrando los de misiones ya existentes;
        PopulateMissionList();
    }


    public void PopulateMissionList()
    {
        DeletePreviousButtons();

        int offset = 0;
        int indexOfMission = 0;

        foreach (Mission mision in missionsLists.retroMissions)
        {

            //spawn botón
            GameObject button = Instantiate(buttonPrefab, buttonPrefab.transform.parent) as GameObject;
            button.SetActive(true);
            presentMissionButtons.Add(button);

            //datos del botón
            button.GetComponent<RetroMissionButton>().missionName.text = mision.missionname;
            button.GetComponent<RetroMissionButton>().missionDescripction.text = mision.desc;
            Double motivationPercent = mision.motivamount *100;
            button.GetComponent<RetroMissionButton>().motivation.text = motivationPercent.ToString() + "%";

            //Los atributos ocultos para distinguir la misión. En este caso, índice de la misión para poder añadir la misión al pulsar el botón
            button.GetComponent<RetroMissionButton>().missionIndex = indexOfMission;

            //Pintamos de verde el botón si la misión se ha añadido ya
            if (ContainsMission(missionsLists.missions, missionsLists.retroMissions, indexOfMission))
            {
                button.GetComponent<Image>().color = Color.green;
            }
            else
            {
                button.GetComponent<Image>().color = Color.white;
            }
            //colocamos el botón en el scroll
            //Cambiamos el tamaño del scroll para que se puedan visualizar correctamente todos los botones de misión
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x,
                                                                    button.GetComponent<RectTransform>().anchoredPosition.y -
                                                                    button.GetComponent<RectTransform>().sizeDelta.y * (offset)
                                                                    - 10 * offset);
            buttonPrefab.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonPrefab.transform.parent.GetComponent<RectTransform>().sizeDelta.x,
                                                                                                 button.GetComponent<RectTransform>().sizeDelta.y * (offset + 1)
                                                                                                 + 10 * (offset + 1) + 20);
            offset = offset + 1;
            indexOfMission = indexOfMission + 1;

        }
    }

    ////////////////////////////////Métodos para la interacción scroll/botones/lista
    public void DeletePreviousButtons()
    {
        //borramos los botones que ya existían
        int presentButtons = buttonPrefab.transform.parent.childCount;
        while (presentButtons > 1)
        {
            Destroy(buttonPrefab.transform.parent.GetChild(presentButtons - 1).gameObject);
            presentButtons -= 1;
        }
    }

    //Se llama desde cada botón de misión
    public void AddMission(int IndexOfMission)
    {
        Debug.Log("Misión " + missionsLists.retroMissions[IndexOfMission].missionname);

        //Así, evitamos añadir la misión por duplicado
        Mission mission = missionsLists.retroMissions[IndexOfMission];
        if (ContainsMission(missionsLists.missions, missionsLists.retroMissions, IndexOfMission))
        {
            Debug.Log("Removing mission: " + mission.missionname);
            missionsLists.missions = RemoveMission(mission, missionsLists.missions);
        }
        else
        {
            missionsLists.missions = RemoveMissionsOfSameType(mission, missionsLists.missions);
            //Añadimos la misión
            missionsLists.missions.Add(mission);
            Debug.Log("Experience mission added: " + mission.missionname);
        }
        Debug.Log("Missions choosen: " + MissionListString(missionsLists.missions));

        PopulateMissionList();
    }

    List<Mission> RemoveMission(Mission missionR, List<Mission> missions)
    {
        List<Mission> r = new List<Mission>();
        foreach (Mission mission in missions)
        {
            if (!mission.Equals(missionR))
            {
                r.Add(mission);
            }
        }

        return r;
    }

    List<Mission> RemoveMissionsOfSameType(Mission mission, List<Mission> missions)
    {
        List<Mission> r = new List<Mission>();
        foreach (Mission m in missions)
        {
            if (m.GetType() != mission.GetType())
            {
                r.Add(m);
            }
        }
        return r;
    }

    bool IsMissionTypeInList(Mission mission, List<Mission> missions)
    {
        bool b = false;
        foreach (Mission m in missions)
        {
            b = m.GetType() == mission.GetType();
            if (b) break;
        }
        return b;
    }

    bool CanBeAdded(Mission mission, List<Mission> missions)
    {
        return !IsMissionTypeInList(mission, missions);
    }

    //parsea una lista de misiones a un string con sus nombres
    public string MissionListString(List<Mission> lista)
    {
        string result = "[";
        foreach (Mission mision in lista)
        {
            result = result + ","+mision.missionname+",";
        }
        result = result + "]";

        return result;
    }

    public bool ContainsMission(List<Mission> listamisiones, List<Mission> listaOrigen, int indexOfMission)
    {
        bool isContained = false;

        Mission misionOrigen = listaOrigen[indexOfMission];

        foreach (Mission mision in listamisiones)
        {
            if (misionOrigen.Equals(mision))
            {
                isContained = true;
                break;
            }
        }

        return isContained;
    }
}

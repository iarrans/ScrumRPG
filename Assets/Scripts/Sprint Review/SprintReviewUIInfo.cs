using ORKFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintReviewUIInfo : MonoBehaviour
{

    public int expTotalProyecto;

    public int expRestante;

    public float ratioExp;//%

    public int expTerminada;

    public int tiempoTotal;

    public int turnosRestantes;

    public float ratioTiempo;//%

    public int tiempoEmpleado;

    public bool goodSprint = false;

    public bool teamGoodSprint = false;

    public GameObject sprintReviewUI;

    public GameObject panelUI;

    public MissionsEvaluations missionsLists;

    public GameObject prefabPanelMission;

    public GameObject prefabPanelExpMission;

    public SprintRetro SprintRetrospective;

    public SprintStatistics estadisticas;


    //Textos de la UI
    public Text expTotalProyectotext;
    public Text expRestantetext;
    public Text expTerminadaText;
    public Text tiempoTotaltext;
    public Text turnosRestantestext;
    public Text tiempoEmpleadoText;
    public Text ratioComparing;
    public Text sprintResultTitle;
    public Image face;
    public Image teamFace;
    public Sprite goodface;
    public Sprite badface;

    private void Start()
    {
        sprintReviewUI.GetComponent<Canvas>().enabled = false;
    }
    public void ShowDataOnConsole()
    {
        Debug.Log("Tiempo total del proyecto: " + tiempoTotal + " turnos");
        Debug.Log("Sprint ended at turn: " + (tiempoTotal - turnosRestantes));
        Debug.Log("Time ratio: " + ratioTiempo);
        Debug.Log("Exp total del proyecto: " + expTotalProyecto);
        Debug.Log("Exp restante: " + expRestante);
        Debug.Log("Ratio de Exp: " + ratioExp);

    }

    public void SetAttributes(int expTotalProyecto, int expRestante, float ratioExp, int expTerminada, int tiempoTotal, int turnosRestantes, float ratioTiempo, int tiempoEmpleado, bool teamEvaluation)
    {
        this.expTotalProyecto = expTotalProyecto;
        this.expRestante = expRestante;
        this.ratioExp = (float)Math.Round(ratioExp * 100, 2);
        this.expTerminada = expTerminada;
        this.tiempoTotal = tiempoTotal;
        this.turnosRestantes = turnosRestantes;
        this.ratioTiempo = (float)Math.Round(ratioTiempo * 100, 2);
        this.tiempoEmpleado = tiempoEmpleado;

        this.teamGoodSprint = teamEvaluation;

        //Administramos recompensas y preparamos toda la información antes de hacer visible la vista
        ShowDataOnConsole();
        RatioEvaluation(this.ratioTiempo, this.ratioExp);
        SetUITexts();
        missionsLists.DoAllMissionsEvaluations(this.expTerminada, this.tiempoEmpleado);
        SetUIMisions();
        sprintReviewUI.GetComponent<Canvas>().enabled = true;
        panelUI.SetActive(true);
        prefabPanelMission.SetActive(false);

        //Se actualizan las estadísticas del Sprint que no han sido seteadas todavía
        estadisticas.numberOfTurns.Add(tiempoEmpleado);
        estadisticas.turnsPercentage.Add((float)Math.Round((float)tiempoEmpleado / (float)tiempoTotal, 2)); //redondeado a dos décimas
        estadisticas.phDefeated.Add(expTerminada / 10); //dividido entre 10 para ajustarse a lo que ve el usuario
        estadisticas.phPercentage.Add((float)Math.Round((float)expTerminada / (float)expTotalProyecto, 2));
        estadisticas.UpdateAverageHealth();
        estadisticas.PrintStats();
    }

    private void RatioEvaluation(float timeRatio, float expRatio)
    {
        if (expRatio < timeRatio)
        {
            Debug.Log("El Sprint ha salido bien, ya que el ratio de experiencia derrotada, " + expRatio + " es mayor que el ratio de tiempo empleado, " + timeRatio);

            this.goodSprint = true;
        }
        else
        {
            Debug.Log("El Sprint ha salido regular, ya que el ratio de experiencia derrotada, " + expRatio + " es menor que el ratio de tiempo empleado, " + timeRatio);

            this.goodSprint = false;
        }

        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
        foreach (GameObject member in grupoCombatants)
        {
            Combatant combatant = ComponentHelper.GetCombatant(member);
            int value = combatant.Status[1].GetValue();
            int maxValue = combatant.Status[1].GetMaxValue();
            float relativeMotivation = (float) value / maxValue;
            float POBonus = - (1 - timeRatio/100) * relativeMotivation;
            float teamBonus = 0;
            if (goodSprint)
            {
                POBonus = (1 - expRatio / 100) * (1 - relativeMotivation);
            }
            if (teamGoodSprint)
            {
                teamBonus = Mathf.Abs(POBonus) * 0.25f;
            }
            Debug.Log("Sprint Review Bonus: " + (POBonus + teamBonus) + " para " + combatant.GetName());
            int newMotivation = Mathf.RoundToInt(value + maxValue * (POBonus + teamBonus));
            combatant.Status[1].Set(newMotivation, newMotivation);
        }
    }

    private void SetUITexts()
    {
        expTotalProyectotext.text = ((this.expTotalProyecto / 100f).ToString());
        expRestantetext.text = ((this.expRestante / 100f).ToString());
        expTerminadaText.text = (this.expTerminada/100).ToString();

        tiempoTotaltext.text = (tiempoTotal.ToString());
        turnosRestantestext.text = (Mathf.Max(0, turnosRestantes).ToString());
        tiempoEmpleadoText.text = this.tiempoEmpleado.ToString();

        if (goodSprint)
        {
            sprintResultTitle.text = "Success!!";
            ratioComparing.text = ratioExp.ToString()+ " % story points remaining\n" + ratioTiempo.ToString() + " % sprint time remaining";
            face.sprite = goodface;
            estadisticas.poEvaluation.Add(1);
        }
        else
        {
            sprintResultTitle.text = "Failure...";
            ratioComparing.text = ratioExp.ToString() + " % story points remaining\n" + ratioTiempo.ToString() + " % sprint time remaining";
            face.sprite = badface;
            estadisticas.poEvaluation.Add(0);
        }

        if (teamGoodSprint)
        {
            teamFace.sprite = goodface;
        }
        else
        {
            teamFace.sprite = badface;
        }
    }

    private void SetUIMisions()
    {
        DeletePreviousButtons();

        int offset = 0;

        List<Mission> missionslist = missionsLists.missions;

        if (missionslist.Count > 0)
        {
            foreach (Mission mision in missionslist)
            {
                GameObject panel;
                if (offset < 3)
                {
                    panel = Instantiate(prefabPanelMission, prefabPanelMission.transform.parent);
                }
                else
                {
                    panel = Instantiate(prefabPanelExpMission, prefabPanelExpMission.transform.parent);
                    offset = 0;
                }
           
                panel.GetComponent<MissionPanel>().missionName.text = mision.missionname;
                panel.GetComponent<MissionPanel>().missionDescripction.text = mision.desc;
                double motivamountPercent = mision.motivamount * 100;
                panel.GetComponent<MissionPanel>().motivation.text = motivamountPercent.ToString() + "%";
                if (mision.missionflag)
                {
                    panel.GetComponent<MissionPanel>().tick.sprite = panel.GetComponent<MissionPanel>().success;
                }
                else
                {
                    panel.GetComponent<MissionPanel>().tick.sprite = panel.GetComponent<MissionPanel>().failure;
                }
                panel.SetActive(true);
                if (mision.GetType() == typeof(TimeMission))
                {
                    panel.GetComponent<Image>().color = new Color(1, 1, 0, 0.3921569f);
                }
                else
                {
                    panel.GetComponent<Image>().color = new Color(0.3544435f, 0.3349057f, 1, 0.3921569f);
                }
                if (offset != 0)
                {
                    Vector2 position = panel.GetComponent<RectTransform>().anchoredPosition;
                    panel.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
                    panel.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                    panel.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
                    panel.GetComponent<RectTransform>().anchoredPosition = new Vector2(-position.x, position.y);
                }
                offset += 1;
            }
        }
    }

    public void DeletePreviousButtons()
    {
        //borramos los botones que ya existían (si es actualizar la lista)

        int presentButtons = prefabPanelMission.transform.parent.childCount;
        while (presentButtons > 1)
        {
            Destroy(prefabPanelMission.transform.parent.GetChild(presentButtons - 1).gameObject);
            presentButtons -= 1;
        }

    }
    public void BeginRetrospective()
    {
        SprintRetrospective.StartRetrospective();
    }
}

using ORKFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintStatistics : MonoBehaviour
{
    public List<int> poEvaluation; //1 si sprint bueno, 0 si sprint malo

    public List<float> averageHealth; //Vida media del equipo al fin de cada Sprint

    public List<int> numberOfTurns; //Número de turnos para completar cada Sprint

    public List<int> phDefeated; //Número de PH derrotados en ese turno

    public List<float> turnsPercentage; //% de turnos gastados en el sprint

    public List<float> phPercentage; //% PH derrotados en el sprint

    public void Start()
    {
        poEvaluation = new List<int>();
        averageHealth = new List<float>();
        numberOfTurns = new List<int>();
        phDefeated = new List<int>();
        turnsPercentage = new List<float>();
        phPercentage = new List<float>();
    }

    public void PrintStats()
    {
        Debug.Log("Se llega a la parte de las estadísticas");
        Debug.Log("Se ha terminado el Sprint número " + poEvaluation.Count.ToString());
        Debug.Log("La última evaluación del PO almacenada es " + poEvaluation[poEvaluation.Count - 1]);
        Debug.Log("La última media de salud almacenada es " + averageHealth[averageHealth.Count - 1]);
        Debug.Log("El último número de turnos empleados almacenado es " + numberOfTurns[numberOfTurns.Count - 1]);
        Debug.Log("El último número de ph derrotados es de " + phDefeated[phDefeated.Count - 1]);
        Debug.Log("El último ratio de tiempo almacenado es " + turnsPercentage[turnsPercentage.Count - 1]);
        Debug.Log("El último ratio de ph almacenado es " + phPercentage[phPercentage.Count - 1]);

    }


    //Calcula la media de motivacion del equipo al final del turno y la añade a la lista de medias
    public void UpdateAverageHealth()
    {
        //sacar lista de combatants en el grupo de player
        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
        float media = 0;


        foreach (GameObject member in grupoCombatants)
        {

            //Obtenemos el objeto combatant asociado
            Combatant combatant = ComponentHelper.GetCombatant(member);

            //obtenemos la motivación
            float motivation = (float)combatant.Status[1].GetDisplayValue();
            media = media + motivation;

        }

        //calculamos y asignamos la media
        media = media / grupoCombatants.Length;
        if (media < 0) media = 0; //a veces, la motivación es negativa. Si la media es negativa, será 0.
        averageHealth.Add(media);
    }

    //Función que calcula y envía a la pantalla final las estadísticas de la partida.
    public List<float> FinalStatistics()
    {

        List<float> medias = new List<float>();


        double poEvaluationRatio = CalculatePoRatio();
        double averageFinalHealth = CalculateAverageHealth();
        double averagePH = CalculateAveragePH();
        double averageTurns = CalculateAverageTurns();
        double averageTimeRatio = CalculateAverageTimeRatio();
        double averagePHRatio = CalculateAveragePHRatio();

        Debug.Log("Durante la partida, se han hecho " + phPercentage.Count+ " sprints");
        Debug.Log("Han salido favorables el " + (float)Math.Round(poEvaluationRatio * 100, 2) + "% de los sprints, según PO");
        Debug.Log("La motivación media a final de sprints ha sido de " + (float)Math.Round(averageFinalHealth, 2) + " puntos");
        Debug.Log("La media de puntos de historia derrotados por sprint ha sido de " + (float)Math.Round(averagePH, 2) + " puntos");
        Debug.Log("La media de turnos empleados por sprint ha sido del " + (float)Math.Round(averageTurns, 2) + "%");
        Debug.Log("La media de tiempo empleado por sprint ha sido del " + (float)Math.Round(averageTimeRatio * 100, 2) + "%");
        Debug.Log("La media de puntos de historia derrotados, ha sido del " + (float)Math.Round(averagePHRatio * 100, 2) + "%");

        medias.Add((float)Math.Round(poEvaluationRatio * 100, 2));
        medias.Add((float)Math.Round(averageFinalHealth, 2));
        medias.Add((float)Math.Round(averagePH, 2));
        medias.Add((float)Math.Round(averagePHRatio * 100, 2));
        medias.Add((float)Math.Round(averageTurns, 2));
        medias.Add((float)Math.Round(averageTimeRatio * 100, 2));
        

        return medias;

    }

    //Funciones auxiliares que calcula las medias de las estadísticas calculadas, se llaman al final de la partida

    private double CalculatePoRatio()
    {
        int approvals = 0;
        foreach (int evaluation in poEvaluation)
        {
            if (evaluation == 1)
            {
                approvals = approvals + 1;
            }
        }

        double ratio = (double)approvals / poEvaluation.Count;

        return ratio;
    }

    private double CalculateAverageHealth()
    {
        int numberOfSprints = averageHealth.Count;

        float healthSum = 0;

        foreach (float sprintHealth in averageHealth)
        {
            healthSum = healthSum + sprintHealth;
        }

        return (double)healthSum / numberOfSprints;
    }

    private double CalculateAveragePH()
    {
        int numberOfSprints = phDefeated.Count;

        int phfinal = 0;

        foreach (int ph in phDefeated)
        {
            phfinal = phfinal + ph;
        }

        return (double)phfinal / numberOfSprints;
    }

    private double CalculateAverageTurns()
    {
        int totalturns = numberOfTurns.Count;

        int turns = 0;

        foreach (int sprintTurns in numberOfTurns)
        {
            turns = turns + sprintTurns;
        }

        return (double)turns / totalturns;
    }

    private double CalculateAverageTimeRatio()
    {
        int totalratios = turnsPercentage.Count;

        float ratiosum = 0;

        foreach (float ratio in turnsPercentage)
        {
            ratiosum = ratiosum + ratio;
        }

        return (double)ratiosum / totalratios;
    }

    private double CalculateAveragePHRatio()
    {
        int totalratios = phPercentage.Count;

        float ratiosum = 0;

        foreach (float ratio in phPercentage)
        {
            ratiosum = ratiosum + ratio;
        }

        return (double)ratiosum / totalratios;
    }


}


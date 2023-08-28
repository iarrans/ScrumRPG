using ORKFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsEvaluations : MonoBehaviour
{

    public List<Mission> missions = new List<Mission>();
    public List<Mission> retroMissions = new List<Mission>();

    private void Awake()
    {
        
    }

    public void PopulateMissionOptions()
    {
        retroMissions = new List<Mission>();

        TimeMission a = new TimeMission(5, "Rush Sprint", "Finish before 5th turn", false, 0.05);
        TimeMission b = new TimeMission(10, "Quick Sprint", "Finish before 10th turn", false, 0.0375);
        TimeMission c = new TimeMission(15, "Fine Sprint", "Finish before 15th turn", false, 0.025);
        TimeMission d = new TimeMission(20, "Steady Sprint", "Finish before 20th turn", false, 0.0125);

        retroMissions.Add(a);
        retroMissions.Add(b);
        retroMissions.Add(c);
        retroMissions.Add(d);

        ExperienceMission a2 = new ExperienceMission(20000, "Complete Story", "Complete at least 200 Story Points", false, 0.30);
        ExperienceMission b2 = new ExperienceMission(10000, "Unfinished Tale", "Complete at least 100 Story Points", false, 0.15);
        ExperienceMission c2 = new ExperienceMission(5000, "Up to the Plot Twist", "Complete at least 50 Story Points", false, 0.05);
        ExperienceMission d2 = new ExperienceMission(2500, "Prologue", "Complete at least 25 Story Points", false, 0.03);

        retroMissions.Add(a2);
        retroMissions.Add(b2);
        retroMissions.Add(c2);
        retroMissions.Add(d2);

        Debug.Log("Total number of suggested missions: " + retroMissions.Count);
    }

    public void DoAllMissionsEvaluations(int expDefeated, int turn)//Función que va a ir llamando a todas las funciones evaluadoras de misiones de cada tipo
    {
        CheckAllMissions(expDefeated, turn);
    }

    private void CheckAllMissions(int expDefeated, int turn)
    {
        foreach (Mission mision in missions)
        {
            if(mision.GetType() == typeof(ExperienceMission))
            {
                ExperienceMission expMision = mision as ExperienceMission;
                bool flagmission = CheckMission(expMision.expObjective, expDefeated, expMision.motivamount);
                expMision.missionflag = flagmission;
                Debug.Log("Misión de experiencia ha salido: " + expMision.missionflag);
            } else if (mision.GetType() == typeof(TimeMission))
            {
                TimeMission timeMision = mision as TimeMission;
                bool flagmission = CheckMission(turn, timeMision.duration, timeMision.motivamount);
                timeMision.missionflag = flagmission;
                Debug.Log("Misión de tiempo ha salido: " + timeMision.missionflag);
            }
            else
            {
                Debug.Log("Error, ese tipo de misión no se puede evaluar");
            }
            
        }
    }

    public bool CheckMission(int compare1, int compare2, double amountMotivation)
    {
        if (compare1 <= compare2)
        {
            GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
            foreach (GameObject member in grupoCombatants)
            {
                Combatant combatant = ComponentHelper.GetCombatant(member);
                int baseValue = combatant.Status[1].GetBaseValue();
                int value = (int)(combatant.Status[1].GetValue() + combatant.Status[1].GetMaxValue() * amountMotivation);//Si se cumple, aplica la recompensa de motivación
                combatant.Status[1].Set(baseValue, value);
            }
            return true;
        }
        else
        {
            GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
            foreach (GameObject member in grupoCombatants)
            {
                Combatant combatant = ComponentHelper.GetCombatant(member);
                int baseValue = combatant.Status[1].GetBaseValue();
                int value = (int)(combatant.Status[1].GetValue() - combatant.Status[0].GetValue() * 0.1f);//Si no se cumple, resta un 10%
                combatant.Status[1].Set(baseValue, value);
            }
            return false;
        }
    }

    public void EmptyLists()
    {
        missions = new List<Mission>();
}

}

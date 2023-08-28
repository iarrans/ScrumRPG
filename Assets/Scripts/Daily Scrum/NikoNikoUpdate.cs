using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikoNikoUpdate : MonoBehaviour
{
    NikoNikoCalendar nikoNikoCalendar;

    // Start is called before the first frame update
    void Start()
    {
        nikoNikoCalendar = GameObject.FindGameObjectWithTag("NikoNikoCalendar").GetComponent<NikoNikoCalendar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNikoNiko()
    {
        Combatant combatant = ComponentHelper.GetCombatant(gameObject);
        int motivation = combatant.Status[1].GetValue();
        int maxMotivation = combatant.Status[0].GetValue();
        float motivationPercentage = (float)motivation / maxMotivation;
        int energy = combatant.Status[3].GetValue();
        int maxEnergy = combatant.Status[2].GetValue();
        float energyPercentage = (float)energy / maxEnergy;
        float r = Random.Range(energyPercentage - 1, motivationPercentage);
        int nikoValue = Random.Range(-1, 2) + Mathf.RoundToInt(r);
        if (nikoValue < -1)
        {
            nikoValue = -1;
        }
        combatant.Status[13].Set(nikoValue, nikoValue);
        int newMotivation = motivation + 10 * nikoValue;
        combatant.Status[1].Set(newMotivation, newMotivation);

        GetComponent<NikoNikoHistorial>().historialNikoNiko.Add(nikoValue);
        nikoNikoCalendar.IncreaseNumber();
    }
}

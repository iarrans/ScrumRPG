using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantOptionButtonFunction : MonoBehaviour

{

    public ORKFramework.Combatant combatant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyScrumMaster() {
        List<Combatant> grupoCombatants = combatant.Group.GetGroup();
        foreach (Combatant member in grupoCombatants)
        {
            member.Status.Effects.Remove(2,true, false, false);
        }
            combatant.Status.Effects.Add(2, combatant, null, false, false, null, null, null);
        var buttonDelete = GameObject.FindGameObjectsWithTag("Delete");
        foreach (GameObject botonborrar in buttonDelete)
        {
            Destroy(botonborrar);
        }

    }
}

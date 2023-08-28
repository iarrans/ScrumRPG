using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class EnemyParty
{
    public static List<Combatant> GetEnemies()
    {
        List<Combatant> enemyCombatants = new List<Combatant>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Combatant enemyCombatant = ComponentHelper.GetCombatant(enemy);
            enemyCombatants.Add(enemyCombatant);
        }
        return enemyCombatants;
    }
}

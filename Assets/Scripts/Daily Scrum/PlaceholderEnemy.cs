using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class PlaceholderEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ORK.Battle.RemoveCombatant(ComponentHelper.GetCombatant(gameObject), false, true);
    }
}

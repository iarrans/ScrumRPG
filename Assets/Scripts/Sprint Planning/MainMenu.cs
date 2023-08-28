using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    static public void DisplayPartyUI()
    {
        //Bucle para mostrar la interfaz de los personajes
        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
        foreach (GameObject member in grupoCombatants)
        {
            member.GetComponent<CombatantUI>().DisplayPlayerUI(true);
        }
    }

    public void DisplayPartyUINonStatic()
    {
        //Bucle para mostrar la interfaz de los personajes
        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
        foreach (GameObject member in grupoCombatants)
        {
            member.GetComponent<CombatantUI>().DisplayPlayerUI(true);
        }
    }
}

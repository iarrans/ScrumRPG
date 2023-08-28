using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;
using System;
using Random = System.Random;

public class Printeo : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Printear()
    {
        string a = ComponentHelper.GetCombatant(gameObject).GetName();
        Debug.Log("El personaje tiene el nombre-------------------" + a);
        return a;
    }
    public float GetCombatantID()
    {
        float id = ComponentHelper.GetCombatant(gameObject).ID;
        Debug.Log("El personaje tiene el nombre-------------------" + ComponentHelper.GetCombatant(gameObject).GetName());
        Debug.Log("El personaje tiene id-------------------" + ComponentHelper.GetCombatant(gameObject).ID);
        Debug.Log("El personaje tiene realid-------------------" + ComponentHelper.GetCombatant(gameObject).RealID);
        return id;
    }

    //Función que devuelve el nombre de un miembro del equipo dada la id.
    public string GetCombatantName()
    {
        string name = ComponentHelper.GetCombatant(gameObject).GetName();
        return name;
    }

    //Función que devuelve una frase aleatoria para que la diga un personaje del equipo.
    public string RandomQuote()
    {
        string selectedQuote = "";
        
        List<string> quotes = new List<string>()
            {
                "Scrum is about learning fast and build what matters most now, indefinitely.",
                "Scrum is a framework that helps teams work together.",
                "The Scrum master is a person who helps other people to understand Scrum",
                "The Scrum Master is the team role responsible for ensuring the team lives agile values",
                "Sprints are time-boxed periods of one week to one month",
                "It focuses on people, communication and working software",
                "I love our team and how we develop things",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod",
                "I think there's enough number of sentences to test this kind of functions"
            };
        Random rnd = new Random();
        int quoteIndex = rnd.Next(quotes.Count);
        selectedQuote = quotes[quoteIndex];

        return selectedQuote;

    }
}

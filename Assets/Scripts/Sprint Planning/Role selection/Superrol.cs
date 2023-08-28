using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Superrol : MonoBehaviour
{

    public string superrol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSuperrol(string superrol)
    {
        this.superrol = superrol;
        Debug.Log($"El personaje tiene el rol-------------------" + this.superrol);
    }

    public string getSuperrol()
    {
        return this.superrol;
    }

    public void printSuperrol()
    {
        Debug.Log($"El personaje tiene el rol-------------------" + this.superrol);
    }

    public string CheckSuperrol()
    {
        Debug.Log(ComponentHelper.GetCombatant(gameObject).Class.Current.GetName());
        string clase = ComponentHelper.GetCombatant(gameObject).Class.Current.GetName();
        string retorno = "";
        if (clase == this.superrol){
            retorno = "misma";
        }
        else
        {
            retorno = "diferente";
        }
        return retorno;
    }

    public void InitialSuperrol()
    {
        Combatant combatant = ComponentHelper.GetCombatant(gameObject);
      
            string clasesr = combatant.Class.Current.GetName();
            this.superrol = clasesr;
    }

    public void SprintEnding()//Llamada al proyecto cuando termina la batalla
    {
        Project proyecto = GameObject.Find("Proyecto").GetComponent<Project>();
        proyecto.BeginSprintReview();
    }
}

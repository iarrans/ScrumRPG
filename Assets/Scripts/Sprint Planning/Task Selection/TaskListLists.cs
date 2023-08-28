using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;
using System;
using UnityEngine.SceneManagement;

public class TaskListLists : MonoBehaviour
{
    //Listas de INT, porque el método get combatant me da problemas. Imagino que da errores al instanciarlos
    public static List<List<int>> sprintBacklogTasks;
    public static List<List<int>> pBacklogIDs;
    public Group sprintBacklog;
    public GameObject project;
    public bool loaded = false;

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        sprintBacklogTasks = new List<List<int>>();
        pBacklogIDs = new List<List<int>>();
    }

    private void Update()
    {

    }

    //Rellena la lista de enemigos del product backlog.
    public void FillProductBacklog(int difficulty)
    {
        sprintBacklogTasks = new List<List<int>>();
        pBacklogIDs = project.GetComponent<Project>().LoadBacklog(difficulty); //Este método se llama desde la clase proyecto
        loaded = true;
        project.GetComponent<Project>().groupSprint = new List<List<int>>();
    }

    //mueve una tarea del pb a la lista del sprint
    public void AddTaskById(int taskID, int exp)
    {

        //eliminamos la tarea de la lista de tareas pendientes product backlog
        List<int> tarea;
        bool taskexists = false;

        foreach (List<int> task in pBacklogIDs)
        {
            //Se busca la primera tarea del tipo que tenga esa experiencia
            if (task[0] == taskID && task[1] == exp)
            {
                tarea = task;

                //la retiramos de la lista de pendientes del backlog
                taskexists = pBacklogIDs.Remove(tarea);


                //añadimos a la lista del sprint backlog la tarea si quedaban en la list
                if (taskexists) {

                    sprintBacklogTasks.Add(tarea);

                }
                break;
            }
        }
    }

    //mueve una tarea del sprint al pb
    public void ReturnTaskById(int taskID, int exp)
    {
        List<int> tarea;
        bool taskexists = false;

        foreach (List<int> task in sprintBacklogTasks)
        {
            //Se busca la primera tarea del tipo que tenga esa experiencia
            if (task[0] == taskID && task[1] == exp)
            {

                tarea = task;

                //la retiramos de la lista de tareas del sprint
                taskexists = sprintBacklogTasks.Remove(tarea);


                //la añadimos a la lista de t pendientes del backlog
                if (taskexists)
                {
                    pBacklogIDs.Add(tarea);

                }
                break;
            }
        }
    }

    //Dividir una tarea en numDivisiones partes, cada una con la mitad de experiencia
    public void SplitTask(int taskID, int exp, int numDivisiones, string listName)//listName indica de cuál de las dos listas de tareas se trata
    {
        List<int> tarea;

        if (listName == "SB") { 
            foreach (List<int> task in sprintBacklogTasks)
            {
                //Se busca la primera tarea que coincida con la tarea a dividir, tanto en exp como en IDr
                if (task[0] == taskID && task[1] == exp)
                {
                    tarea = task;

                    //Sacamos la tarea original de la lista
                    sprintBacklogTasks.Remove(tarea);

                    //Dividimos la experiencia de la tarea en numDivisiones
                    int exp2 = exp / numDivisiones;

                    //Introducimos numDivisiones tareas idénticas en la lista
                    List<int> newtask = new List<int> { taskID, exp2, 0 };
                    while(numDivisiones > 0)
                    {
                        if (numDivisiones == 1 && exp % 2 != 0)
                        {
                            newtask = new List<int> { taskID, exp2 + 1, 0 };
                        }
                        sprintBacklogTasks.Add(newtask);
                        numDivisiones = numDivisiones - 1;
                    }

                    break;
                }
            }

        } else
        {
            foreach (List<int> task in pBacklogIDs)
            {
                //Se busca la primera tarea que coincida con la tarea a dividir, tanto en exp como en IDr
                if (task[0] == taskID && task[1] == exp)
                {
                    tarea = task;

                    //Sacamos la tarea original de la lista
                    pBacklogIDs.Remove(tarea);


                    //Dividimos la experiencia de la tarea en numDivisiones
                    int exp2 = exp / numDivisiones;
                    //Introducimos numDivisiones tareas idénticas en la lista
                    List<int> newtask = new List<int> { taskID, exp2, 0 };
                    while (numDivisiones > 0)
                    {
                        if (numDivisiones == 1 && exp % 2 != 0)
                        {
                            newtask = new List<int> { taskID, exp2 + 1, 0 };
                        }
                        pBacklogIDs.Add(newtask);

                        numDivisiones = numDivisiones - 1;
                    }

                    break;
                }
            }

        }
    }

    //Busca tareas que sean del mismo tipo y las agrupa en una única tarea, sumando sus experiencias
    public void JoinAllTasks(int taskID, string listName)
    {
        int expCounter = 0;
        List<List<int>> aEliminar = new List<List<int>>();


        if (listName == "SB")
        {
            foreach (List<int> task in sprintBacklogTasks)
            {

                //Se busca la primera tarea que coincida con la tarea a dividir, tanto en exp como en ID
                if (task[0] == taskID && task[2] == 0)
                {
                    //almacenamos la experiencia total
                    expCounter += task[1];
                    //Añadimos la tarea a tareas antiguas
                    aEliminar.Add(task);
                }
            }

            //Elimina las tareas antiguas
            foreach (List<int> tarea in aEliminar)
            {
                sprintBacklogTasks.Remove(tarea);
            }
            if (expCounter == 0)
            {
            }
            else
            {
                List<int> newtask = new List<int> { taskID, expCounter, 0 };
                sprintBacklogTasks.Add(newtask);
            }

        }
        else {

            foreach (List<int> task in pBacklogIDs)
            {

                //Se busca la primera tarea que coincida con la tarea a dividir, tanto en exp como en ID
                if (task[0] == taskID && task[2] == 0)
                {
                    //almacenamos la experiencia total
                    expCounter += task[1];
                    //Añadimos la tarea a tareas antiguas
                    aEliminar.Add(task);
                }
            }

            //Elimina las tareas antiguas
            foreach (List<int> tarea in aEliminar)
            {
                pBacklogIDs.Remove(tarea);

            }
            if (expCounter == 0)
            {
            }
            else
            {
                List<int> newtask = new List<int> { taskID, expCounter, 0 };
                pBacklogIDs.Add(newtask);

            }

        }
    }

    //Función para unir dos tareas del mismo ID de la lista. Solo se pasa una vez el ID porque es el mismo para las dos tareas
    public void Join2Tasks(int taskID,int exp, int exp2)
    {
        
        List<int> tarea1 = new List<int> {};
        List<int> tarea2 = new List<int> {};
        bool removable1 = false;
        bool removable2 = false;

        foreach (List<int> task in sprintBacklogTasks)
        {
            //Se busca la primera tarea que coincida con la primera tarea
            if (task[0] == taskID && task[1] == exp)
            {
                tarea1 = task;
                //Sacamos la tarea original de la lista
                removable1 = sprintBacklogTasks.Remove(tarea1);
                break;
            }
        }

        foreach (List<int> task in sprintBacklogTasks)
        {
            //Se busca la primera tarea que coincida con la primera tarea
            if (task[0] == taskID && task[1] == exp2)
            {
                tarea2 = task;
                //Sacamos la tarea original de la lista
                removable2 = sprintBacklogTasks.Remove(tarea2);
                break;
            }
        }

        //casos si encuentra enla lista original las 2 tareas, una de ellas o ninguna
        if (removable1 && removable2) {
            List<int> newtask = new List<int> { taskID, exp + exp2, 0 };
            sprintBacklogTasks.Add(newtask);
        } else if (removable1) {
            sprintBacklogTasks.Add(tarea1);
        }
        else if(removable2)
        {
           sprintBacklogTasks.Add(tarea2);
        }
        else
        {

        }
    }


    public string GetPBTaskList(List<List<int>> pBacklogIDs)
    {
        //Simplemente, función para printear por consola la lista de ids reales de las tareas que quedan en el backlog
        string idList = "[ ";
        foreach (List<int> task in pBacklogIDs)
        {
            idList += "" + task[0] + "[" + task[1] + "],";
        }

        idList += " ]";
        return idList;
    }

    //Crear un grupo de combatientes con la lista de IDs que se ha decidido
    public Group CreateCombatantGroup()
    {
        sprintBacklog = new Group(1);

        foreach (List<int> task in sprintBacklogTasks)
        {
            Combatant tarea = ORK.Access.Combatant.CreateInstance(task[0], sprintBacklog, true, true);
            Debug.Log(task[1]);
            tarea.Status[10].SetBaseValue(task[1]);
            tarea.Status[10].SetBaseValueAccess(task[1]);
            tarea.Status.Level = LevelUp(tarea, task[1]);
            tarea.RenameableName = tarea.GetName() + " " + (task[1] / 100f);
            task.Add(tarea.ID);
            project.GetComponent<Project>().groupSprint.Add(task);
        }

        //Al acabar el sprint planning y empezar el combate, se guardan los cambios en las tareas del backlog en la clase proyecto
        project.GetComponent<Project>().productBacklog = pBacklogIDs;
        project.GetComponent<Project>().calledEndBattle = false;

        Debug.Log("Enemigos PB proyecto:  " + GetPBTaskList(project.GetComponent<Project>().productBacklog));
        Debug.Log("Enemigos pendientes:  " + GetPBTaskList(pBacklogIDs));

        return sprintBacklog;

        //función iniciar combate contra grupo x
    }

    int LevelUp(Combatant task, int exp)
    {
        int level = task.Status.MaxLevel;
        while (level > 0)
        {
            if (exp >= task.Status.GetValueAtLevel(10, level))
            {
                break;
            }
            level -= 1;
        }
        int attack = task.Status.GetValueAtLevel(4, level);
        int accomplishment = task.Status.GetValueAtLevel(6, level);
        task.Status[4].SetBaseValue(attack);
        task.Status[6].SetBaseValue(accomplishment);
        return level;
    }

    public void AddParty()
    {
        List<Combatant> list = sprintBacklog.GetGroup();
        ORK.Battle.Join(list);
    }

    public string GetCombatantsList(Group group)
    {
        //Simplemente, función para printear por consola la lista de ids reales de los prefabs de los combatientes del grupo
        string idList = "[ ";
        List<Combatant> grupo = group.GetGroup();
        foreach (Combatant task in grupo)
        {
            int ID = task.RealID;
            int exp = task.Status[10].GetBaseValue();
            idList += "" + ID + "[" + exp + "],";
        }

        idList += " ]";
        return idList;
    }
    ///funciones para la UI/////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///


    public List<List<int>> GetProductBacklogList()
    {
        return pBacklogIDs;
    }

    public List<List<int>> GetSprintBacklogList()
    {
        return sprintBacklogTasks;
    }

    public void GetCombatantInfoConsole(int taskID)
    {
        Debug.Log("Combatant name: " + GetTaskNameID(1));
    }

    //Métodos auxiliares para obtener la información para la UID de elegir tareas de los combatants
    public string GetTaskNameID(int taskID){

        string combatant = ORK.Combatants.Get(taskID).GetName();
        return combatant;
    }

    public void ChangeExperiencie(int taskID)
    {
        Combatant combatant = ORK.Game.Combatants.Get(taskID);//Es que este puñetero da fallo
        combatant.Status[10].SetBaseValue(100); //[idstatus] idstatus= id experiencia = 10
    }

   public void EmptySB()
    {
        sprintBacklogTasks = new List<List<int>>();
    }
    
}

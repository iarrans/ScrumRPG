using ORKFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Project : MonoBehaviour
{

    public List<List<int>> productBacklog;

    private int totalExperience;

    private int pendingExperience;

    private int projectTotalTurns;

    private int projectRemainingTurns;

    private int actualTurn;

    public List<List<int>> groupSprint;//lista de listas de int(realid, groupid, exp)

    public int actualWeek;

    public int sprintExperience; //para los ratios de experiencia derrotada en cada sprint

    public bool calledEndBattle = false;//para que el evento del final del combate solo se llame una vez

    private GameObject[] pendingEnemies;

    public GameObject SprintReview;

    public int sprintBacklogExperience = 0;


    private void Start()
    {
        productBacklog = new List<List<int>>();
        groupSprint = new List<List<int>>();
        ORK.Battle.EnemyKilled += Battle_EnemyKilled;
        sprintExperience = 0;
    }

    private void Update()
    {
        if (ORK.Battle.InBattle)
        {
            actualTurn = ORK.Battle.Turn; //Permite obtener el turno actual para los ratios
        }
    }

    public List<List<int>> LoadBacklog(int difficulty)
    {
        RestDBAPI api = GameObject.Find("Proyecto").GetComponent<RestDBAPI>();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Trial Map") //Lee la escena para saber qué lista cargar. Como mejora, podemos hacer un método auxiliar que setee los atributos del combate y haga los logs
        {
            List<int> sampletask;

            switch (difficulty)
            {
                case 0:

                    api.difficulty = "facil";

                    //Misiones de 3

                    sampletask = new List<int> { 5, 300, 1 };  //{ X, Y, Z } X: Id combatiente; Y: Experiencia tarea (PH x100); Z: 0 = Divisible/unificable, !0 = No divisible/unificable
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 300, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 300, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 300, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 300, 0 };
                    productBacklog.Add(sampletask);

                   

                    //Misiones de 5

                    sampletask = new List<int> { 5, 500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 500, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 500, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 500, 0 };
                    productBacklog.Add(sampletask);

                    //Misiones de 10
                    sampletask = new List<int> { 5, 1000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 1000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 1000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 1000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 1000, 0 };
                    productBacklog.Add(sampletask);
                    break;
                case 1:

                    api.difficulty = "media";

                    //Misiones de 3

                    sampletask = new List<int> { 5, 300, 1 };  //{ X, Y, Z } X: Id combatiente; Y: Experiencia tarea (PH x100); Z: 0 = Divisible/unificable, !0 = No divisible/unificable
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 300, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 300, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 300, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 300, 0 };
                    productBacklog.Add(sampletask);

                    //Misiones de 5

                    sampletask = new List<int> { 5, 500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 500, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 500, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 500, 0 };
                    productBacklog.Add(sampletask);

                    
                    //Misiones de 10
                    sampletask = new List<int> { 5, 1000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 1000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 1000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 1000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 1000, 0 };
                    productBacklog.Add(sampletask);

                    //Misiones de 15
                    sampletask = new List<int> { 5, 1500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 1500, 1 };
                    productBacklog.Add(sampletask);

                    //Misiones de 20
                    sampletask = new List<int> { 6, 2000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 2000, 0 };
                    productBacklog.Add(sampletask);
                    break;
                    
                case 2:

                    api.difficulty = "dificil";
                    //Misiones de 5

                    sampletask = new List<int> { 5, 500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 500, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 500, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 500, 0 };
                    productBacklog.Add(sampletask);


                    //Misiones de 10
                    sampletask = new List<int> { 5, 1000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 6, 1000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 1000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 1000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 1000, 1 };
                    productBacklog.Add(sampletask);

                    //Misiones de 15
                    sampletask = new List<int> { 6, 1500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 7, 1500, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 1500, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 8, 1500, 0 };
                    productBacklog.Add(sampletask);

                    //Misiones de 20
                    sampletask = new List<int> { 6, 2000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 9, 2000, 1 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 5, 2000, 0 };
                    productBacklog.Add(sampletask);

                    //Misiones de 30
                    sampletask = new List<int> { 7, 3000, 0 };
                    productBacklog.Add(sampletask);

                    sampletask = new List<int> { 5, 3000, 0 };
                    productBacklog.Add(sampletask);
                    break;
            }


            this.projectTotalTurns = 50;
            this.projectRemainingTurns = projectTotalTurns;

            totalExperience = CountExperience(productBacklog);
            pendingExperience = CountExperience(productBacklog);
            this.actualTurn = 0;
        }

        return productBacklog;
    }

    public int GetTotalExperience()
    {
        return this.totalExperience;
    }

    public int CountExperience(List<List<int>> tasklist)
    {
        int counter = 0;

        foreach (List<int> task in tasklist)//Recorremos las tareas para sumar la experiencia
        {
            counter = counter + task[1];
        }

        return counter;
    }

    public int GetProjectTurns()
    {
        return this.projectTotalTurns;
    }

    public int GetCurrentExperience()
    {
        int counter = 0;
        foreach (List<int> task in productBacklog)//Recorremos las tareas para sumar la experiencia de las tareas no eliminadas
        {
            counter = counter + task[1];
        }
        return counter;//Experiencia total - experiencia restante = experiencia eliminada
    }


    // Evento que se activa cada vez que un enemigo es eliminado en batalla
    private void Battle_EnemyKilled(Combatant combatant)
    {
        Debug.Log(string.Format("{0} completed!!", combatant.GetName()));
       
        //se busca tarea que coincida en RealID y GroupID con un elemento de la lista y se elimina
        foreach(List<int> task in groupSprint)
        {
            if (task[0]==combatant.RealID && task[3]==combatant.ID)
            {
                groupSprint.Remove(task);
                sprintExperience = sprintExperience + task[1]; //Añadimos la experiencia derrotada a la exp del sprint
                pendingExperience = pendingExperience - task[1];
                break;
            }
        }
    }


    //Se le llama desde la clase superrol de los combatants al terminar un combate;
    public void BeginSprintReview()
    {
        if (calledEndBattle == false)//Como el evento se llama una vez por combatant, este booleano hará que solo se llame una vez
        {
            ORK.Control.EnablePlayerControls(false);

            GameObject[] party = GameObject.FindGameObjectsWithTag("GroupCombatant");
            foreach (GameObject ally in party)
            {
                ally.transform.position = new Vector3(100000, 100000);
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }

            Sprint sprint = GameObject.FindGameObjectWithTag("BattleMain").GetComponent<Sprint>();
            sprint.EmptySprint();

            calledEndBattle = true;

            bool sprintBacklogFinished = sprintBacklogExperience <= sprintExperience;

            if (!sprintBacklogFinished)
            {
                actualTurn -= 1;
            }

            projectRemainingTurns -= actualTurn;

            float timeRatio = TimeRatio();

            AddGroupSprintToBacklog();//Añade las tareas que no se terminaron durante el combate de vuelta al backlog

            float expRatio = ExperienceRatio();

            //Pasamos los atributos necesarios para las estadísticas al script de Sprint Review
            SprintReview.GetComponent<SprintReviewUIInfo>().SetAttributes(this.GetTotalExperience(), pendingExperience, expRatio, sprintExperience, this.projectTotalTurns, 
                projectRemainingTurns, timeRatio, actualTurn, sprintBacklogFinished);
            sprintExperience = 0;

            GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
            //Bucle para PB
            foreach (GameObject member in grupoCombatants)
            {
                member.GetComponent<CombatantUI>().DisplayPlayerUI(true);
            }
        }
    }

    private void AddGroupSprintToBacklog()
    {
        if (groupSprint.Count > 0) {
            foreach (List<int> task in groupSprint)
            {
                productBacklog.Add(task);
            }
        }
        groupSprint = new List<List<int>>();
        
    }

    public float ExperienceRatio() //ratio de experiencia terminada respecto a la experiencia pendiente
    {
        float ratio = (float)pendingExperience/ (float)this.GetTotalExperience();

        return ratio;
    }

    public float TimeRatio() //ratio del tiempo consumido hasta ahora en el proyecto
    {
        float ratio = (float) projectRemainingTurns / projectTotalTurns;

        return ratio;
    }

    public int GetRemainingTurns() //Número de turnos restantes
    {
        return this.projectRemainingTurns;
    }



}

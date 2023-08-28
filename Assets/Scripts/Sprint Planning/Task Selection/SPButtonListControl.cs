using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPButtonListControl : MonoBehaviour
{

    //Prefab del botón que se va a construir
    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private GameObject buttonTemplateSprint;

    private bool listagenerada = false;

    public GameObject tasklistcontroller;

    //Lista de botones presentes en escena
    public List<GameObject> buttonsPB;
    public List<GameObject> buttonsSB;

    //indica la distancia que debe haber entre cada botón al añadirlo en el scroll
    public int buttonSize = 71;

    //Llamada al código que tiene las funciones de tasklistlists
    //public TaskListLists tasksController;

    
    private void Start()
    {
        //ponemos botones a 0, porque todavía no se ha cargado ningún episodio
        buttonsPB = new List<GameObject>();

        buttonsSB = new List<GameObject>();

    }

    
    //función para generar los botones de Product Backlog Task
    public void PopulateButtonList() {

        //Bucle para ocultar la interfaz de los personajes
        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
        foreach (GameObject member in grupoCombatants)
        {
            member.GetComponent<CombatantUI>().DisplayPlayerUI(false);
        }

        //instanciamos, en bucle, un botón por cada tarea del product backlog  y otro por cada tarea en el sprint backlog

        List<List<int>> tasksPB = tasklistcontroller.GetComponent<TaskListLists>().GetProductBacklogList();

        List<List<int>> tasksSB = tasklistcontroller.GetComponent<TaskListLists>().GetSprintBacklogList();


        DeletePreviousButtons();

        int distanciaY = 0;

        //Bucle para PB
        foreach (List<int> task in tasksPB)
        {
            GameObject button = Instantiate(buttonTemplate, buttonTemplate.transform.parent) as GameObject;
            if (task[2] != 0)
            {
                button.transform.GetChild(3).gameObject.SetActive(false);
                button.transform.GetChild(5).gameObject.SetActive(false);
            }
            button.tag = "Delete";
            button.SetActive(true);
            buttonsPB.Add(button);

            //id y experiencia a mostrar
            string taskID = task[0].ToString();
            int exp = task[1];
            string taskName = tasklistcontroller.GetComponent<TaskListLists>().GetTaskNameID(task[0]);

            button.GetComponent<SPTaskListButton>().SetName(taskID); //a lo mejor sustituir la ID por nombre y añadir en botón un componente oculto ID??
            button.GetComponent<SPTaskListButton>().SetExp(exp);
            button.GetComponent<SPTaskListButton>().SetTaskName(taskName);
            button.GetComponent<SPTaskListButton>().SetRoleTags(task[0]);


            //para la posición del botón. Relativizamos a botón padre y desplazamos hacia abajo
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x,
                                                                                button.GetComponent<RectTransform>().anchoredPosition.y -
                                                                                button.GetComponent<RectTransform>().sizeDelta.y * (distanciaY)
                                                                                - 10 * distanciaY);
            buttonTemplate.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonTemplate.transform.parent.GetComponent<RectTransform>().sizeDelta.x,
                                                                                                  button.GetComponent<RectTransform>().sizeDelta.y * (distanciaY + 1)
                                                                                                  + 10 * (distanciaY + 1) + 20);
            distanciaY = distanciaY + 1;

        }

        //reseteamos distancia Y
        distanciaY = 0;

        //Bucle para SB
        foreach (List<int> task in tasksSB)
        {
            GameObject button = Instantiate(buttonTemplateSprint, buttonTemplateSprint.transform.parent) as GameObject;
            if (task[2] != 0)
            {
                button.transform.GetChild(3).gameObject.SetActive(false);
                button.transform.GetChild(5).gameObject.SetActive(false);
            }
            button.tag = "Delete";
            button.SetActive(true);
            buttonsSB.Add(button);

            //id y experiencia a mostrar
            string taskID = task[0].ToString();
            int exp = task[1];
            string taskName = tasklistcontroller.GetComponent<TaskListLists>().GetTaskNameID(task[0]);


            button.GetComponent<SPTaskListButton>().SetName(taskID); //a lo mejor sustituir la ID por nombre y añadir en botón un componente oculto ID??
            button.GetComponent<SPTaskListButton>().SetExp(exp);
            button.GetComponent<SPTaskListButton>().SetTaskName(taskName);
            button.GetComponent<SPTaskListButton>().SetRoleTags(task[0]);

            //para la posición del botón. Relativizamos a botón padre y desplazamos hacia abajo
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x,
                                                                                button.GetComponent<RectTransform>().anchoredPosition.y - 
                                                                                button.GetComponent<RectTransform>().sizeDelta.y * (distanciaY)
                                                                                - 10 * distanciaY);
            buttonTemplateSprint.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonTemplate.transform.parent.GetComponent<RectTransform>().sizeDelta.x,
                                                                                                 button.GetComponent<RectTransform>().sizeDelta.y 
                                                                                                 + 10 * (distanciaY + 1));
            distanciaY = distanciaY + 1;
        }

    }

    ////////////////////////////////Métodos para la interacción scroll/botones/lista

    public void AddTaskToSprint(string id, string exp, GameObject button)
    {
        int taskID = int.Parse(id);
        float taskExpF = float.Parse(exp) * 100;
        int taskExp = Mathf.RoundToInt(taskExpF);
        tasklistcontroller.GetComponent<TaskListLists>().AddTaskById(taskID, taskExp);
        buttonsSB.Add(button);
        buttonsPB.Remove(button);
        PopulateButtonList();
    }

    public void RemoveTaskFromSprint(string id, string exp, GameObject button)
    {
        int taskID = int.Parse(id);
        float taskExpF = float.Parse(exp) * 100;
        int taskExp = Mathf.RoundToInt(taskExpF);
        tasklistcontroller.GetComponent<TaskListLists>().ReturnTaskById(taskID, taskExp);
        buttonsPB.Add(button);
        buttonsSB.Remove(button);
        PopulateButtonList();
    }

    public void DeletePreviousButtons()
    {
        //borramos los botones que ya existían (si es actualizar la lista)

        int presentButtons = buttonTemplateSprint.transform.parent.childCount;
        while (presentButtons > 1)
        {
            Destroy(buttonTemplateSprint.transform.parent.GetChild(presentButtons - 1).gameObject);
            presentButtons -= 1;
        }


        int presentButtonsPB = buttonTemplate.transform.parent.childCount;
        while (presentButtonsPB > 1)
        {
            Destroy(buttonTemplate.transform.parent.GetChild(presentButtonsPB - 1).gameObject);
            presentButtonsPB -= 1;
        }

    }

    public void DivideTaskBy2(int taskID, int exp, string listname)
    {
        tasklistcontroller.GetComponent<TaskListLists>().SplitTask(taskID, exp, 2, listname);
        PopulateButtonList();
    }

    public void JoinTasks(int taskID, string listname)
    {
        tasklistcontroller.GetComponent<TaskListLists>().JoinAllTasks(taskID, listname);
        PopulateButtonList();
    }

}

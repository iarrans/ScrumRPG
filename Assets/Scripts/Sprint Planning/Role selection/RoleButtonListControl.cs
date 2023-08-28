using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;
using UnityEngine.UI;

public class RoleButtonListControl : MonoBehaviour
{
    //Prefab del botón que se va a construir
    [SerializeField]
    private GameObject buttonTemplate;
    //Lista de botones presentes en escena
    public List<GameObject> buttonsCombatants;

    private void Start()
    {
        //ponemos botones a 0, porque todavía no se ha cargado ningún episodio
        buttonsCombatants = new List<GameObject>();
    }

    //función para generar los botones de Product Backlog Task
    public void PopulateButtonList()
    {
        DeletePreviousButtons();

        //sacar lista de combatants en el grupo de player
        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");

        int distanciaX = 0;

        //Bucle para PB
        foreach (GameObject member in grupoCombatants)
        {
            Debug.Log(member.ToString());

            GameObject button = Instantiate(buttonTemplate, buttonTemplate.transform.parent) as GameObject;
            button.SetActive(true);
            buttonsCombatants.Add(button);

            member.GetComponent<CombatantUI>().DisplayPlayerUI(false);

            Combatant combatant = ComponentHelper.GetCombatant(member);
            //datos a mostrar
            string name = combatant.GetName();
            string activity = combatant.Class.Current.GetName();
            Sprite sprite = member.GetComponent<SpriteRenderer>().sprite;

            string superrol = member.GetComponent<Superrol>().getSuperrol();
           

            button.GetComponent<RoleListButton>().SetName(name);
            button.GetComponent<RoleListButton>().SetCurrentRole(superrol);
            button.GetComponent<RoleListButton>().SetCombatant(member);
            button.GetComponent<RoleListButton>().SetCombatantSprite(sprite);
            List<int> levels = new List<int>();
            ExtendedCombatant extendedCombatant = member.GetComponent<ExtendedCombatant>();
            levels.Add(extendedCombatant.developerLevel);
            levels.Add(extendedCombatant.qualityAssuranceLevel);
            levels.Add(extendedCombatant.testerLevel);
            levels.Add(extendedCombatant.UIDesignerLevel);
            levels.Add(extendedCombatant.deploymentLevel);
            button.GetComponent<RoleListButton>().SetRoleLevels(levels);

            //para la posición del botón. Relativizamos a botón padre y desplazamos hacia la derecha
            Vector2 size = button.GetComponent<RectTransform>().sizeDelta;
            button.GetComponent<RectTransform>().anchoredPosition = button.GetComponent<RectTransform>().anchoredPosition + 
                                                                    new Vector2(size.x * (distanciaX) + 10 * distanciaX, 0 );
            buttonTemplate.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x * (distanciaX + 1) + 10 * (distanciaX + 1),
                                                                                      buttonTemplate.transform.parent.GetComponent<RectTransform>().sizeDelta.y);
            distanciaX = distanciaX + 1;
        }
    }

    ////////////////////////////////Métodos para la interacción scroll/botones/lista
    public void DeletePreviousButtons()
    {
        //borramos los botones que ya existían (si es actualizar la lista)

        int presentButtons = buttonTemplate.transform.parent.childCount;
        while (presentButtons > 1)
        {
            Destroy(buttonTemplate.transform.parent.GetChild(presentButtons - 1).gameObject);
            presentButtons -= 1;
        }

    }

    public void SetSuperrol(GameObject member, string superrol)
    {
        //Cambiamos su superrol al del botón pulsado
        member.GetComponent<Superrol>().setSuperrol(superrol);

        //Cambiamos su clase también en ORK, para que empiece en su rol correspondiente

        Combatant combatant = ComponentHelper.GetCombatant(member); 
        if (superrol=="Developer")
        {
            combatant.Class.Change(0,false, true, true, false, true, true, true, true);

        }else if (superrol == "Quality Assurance")
        {
            combatant.Class.Change(1, false, true, true, false, true, true, true, true);
        }
        else if (superrol == "Tester")
        {
            combatant.Class.Change(2, false, true, true, false, true, true, true, true);
        }
        else if (superrol == "UI Designer")
        {
            combatant.Class.Change(3, false, true, true, false, true, true, true, true);
        }
        else
        {
            combatant.Class.Change(4, false, true, true, false, true, true, true, true);
        }

        PopulateButtonList();
    }
}

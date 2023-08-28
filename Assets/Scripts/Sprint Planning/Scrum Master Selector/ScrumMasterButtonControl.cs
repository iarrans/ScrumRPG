using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;
using UnityEngine.UI;

public class ScrumMasterButtonControl : MonoBehaviour
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
            GameObject button = Instantiate(buttonTemplate, buttonTemplate.transform.parent) as GameObject;
            button.SetActive(true);
            buttonsCombatants.Add(button);

            member.GetComponent<CombatantUI>().DisplayPlayerUI(false);

            Combatant combatant = ComponentHelper.GetCombatant(member);
            //datos a mostrar
            string name = combatant.GetName();
            string superrol = member.GetComponent<Superrol>().getSuperrol();
            float motivation = (float)combatant.Status[1].GetDisplayValue() / (float)combatant.Status[0].GetDisplayValue(); ;
            float energy = (float)combatant.Status[3].GetDisplayValue() / (float)combatant.Status[2].GetDisplayValue();
            Sprite sprite = member.GetComponent<SpriteRenderer>().sprite;

            button.GetComponent<ScrumMasterButton>().SetName(name);
            button.GetComponent<ScrumMasterButton>().SetCurrentRole(superrol);
            button.GetComponent<ScrumMasterButton>().SetMotivation(motivation);
            button.GetComponent<ScrumMasterButton>().SetEnergy(energy);
            button.GetComponent<ScrumMasterButton>().SetCombatant(member);
            button.GetComponent<ScrumMasterButton>().SetCombatantSprite(sprite);

            //Coloreamos el botón correspondiente al Scrum Master del grupo, para que sea más diferenciable
            List<StatusEffect> statusEffects = combatant.Status.Effects.GetEffects();

            foreach (StatusEffect statusEffect in statusEffects)
            {
                if (statusEffect.ID == 2)
                {
                    button.GetComponent<Image>().color = Color.cyan;
                }
            }


            //para la posición del botón. Relativizamos a botón padre y desplazamos hacia la derecha
            Vector2 size = button.GetComponent<RectTransform>().sizeDelta;
            button.GetComponent<RectTransform>().anchoredPosition = button.GetComponent<RectTransform>().anchoredPosition +
                                                                    new Vector2(size.x * (distanciaX) + 10 * distanciaX, 0);
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

    public void SetScrumMaster(GameObject combatant)
    {
        //Quitamos el estado ScrumMaster a todos los miembros del equipo, para evitar duplicados
        GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
        foreach (GameObject member in grupoCombatants)
        {
            Combatant memberCombatant = ComponentHelper.GetCombatant(member);
            memberCombatant.Status.Effects.Remove(2, true, false, false);
        }

        //aplicamos el efecto sobre el combatiente del botón
        Combatant combatantElegido = ComponentHelper.GetCombatant(combatant);
        combatantElegido.Status.Effects.Add(2, combatantElegido, null, false, false, null, null, null);

        //refrescamos la lista de botones
        Debug.Log(combatantElegido.GetName() + " ha sido convertido a ScrumMaster con éxito");
        PopulateButtonList();
    }

}

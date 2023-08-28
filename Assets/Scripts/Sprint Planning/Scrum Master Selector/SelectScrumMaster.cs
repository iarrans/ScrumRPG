using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectScrumMaster : MonoBehaviour
{

    public GameObject button;
    public GameObject canvas;
    public Vector2 initialPosition;
    private int wait = 30;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateCombatantButtons(){
        Combatant combatant = ComponentHelper.GetCombatant(gameObject);
        List<Combatant> grupoCombatants = combatant.Group.GetGroup();
        Debug.Log(grupoCombatants.Count);
        int offset = 0;
        GameObject buttonCanvas = Instantiate(canvas);
        foreach (Combatant member in grupoCombatants ) {

            GameObject boton = Instantiate(button, buttonCanvas.transform);
            boton.GetComponent<RectTransform>().anchoredPosition = new Vector2(initialPosition.x, initialPosition.y - 50 * offset);
            boton.transform.GetChild(0).GetComponent<Text>().text = member.GetName();
            boton.GetComponent<CombatantOptionButtonFunction>().combatant = member;

            offset += 1;
        }
    }
}

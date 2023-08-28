using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ORKFramework;

public class NikoNikoMemberEntry : MonoBehaviour
{
    public GameObject member;

    public Text memberName;
    public GameObject nikoNikoSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateMemberEntry()
    {
        memberName.text = ComponentHelper.GetCombatant(member).GetName();
        int i = 0;
        foreach (int nikoNiko in member.GetComponent<NikoNikoHistorial>().historialNikoNiko)
        {
            GameObject icon = NikoNikoCalendar.GenerateObject(nikoNikoSprite, i, transform);
            icon.GetComponent<NikoNikoEntry>().ChangeSpirte(nikoNiko);
            i += 1;
        }
    }
}

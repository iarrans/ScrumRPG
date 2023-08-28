using ORKFramework;
using ORKFramework.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrumMasterAbilitiesButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool activate = true;
        if (ORK.Game.Variables.GetBool("hasUsedScrumMasterAbility"))
        {
            activate = false;
        }
        GetComponent<Button>().interactable = activate;
        GetComponent<UIPoolComponent>().enabled = activate;
        GetComponent<UIChoiceButtonComponent>().enabled = activate;
        GetComponent<UIClickComponent>().enabled = activate;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleListButton : MonoBehaviour
{
    [SerializeField]
    private Text combatantName;

    [SerializeField]
    private Text currentRole;

    [SerializeField]
    private RoleButtonListControl buttonControl;

    [SerializeField]
    private GameObject combatant;

    [SerializeField]
    private Image combatantSprite;

    [SerializeField]
    private Image roleBackground;

    [SerializeField]
    private List<Text> roleTexts;

    private string nameString;
    

    //setters
    public void SetName(string nombre)
    {
        combatantName.text = nombre;
    }

    public void SetCurrentRole(string role)
    {
        currentRole.text = role;
        switch (role)
        {
            case "Developer":
                roleBackground.color = RoleTags.developerColor;
                break;
            case "Quality Assurance":
                roleBackground.color = RoleTags.qualityAssuranceColor;
                break;
            case "Tester":
                roleBackground.color = RoleTags.testerColor;
                break;
            case "UI Designer":
                roleBackground.color = RoleTags.UIDesignerColor;
                break;
            case "Deployment":
                roleBackground.color = RoleTags.deploymentColor;
                break;
        }
    }

    public void SetCombatant(GameObject combatante)
    {
        combatant = combatante;
    }

    public void SetCombatantSprite(Sprite combatanteSprite)
    {
        combatantSprite.sprite = combatanteSprite;
    }


    //Método para cambiar el superrol y rol de un miembro del equipo
    public void SetSuperrol(string superrol)
    {
        buttonControl.SetSuperrol(combatant, superrol);
    }

    public void SetRoleLevels(List<int> levels)
    {
        int i = 0;
        while (i < roleTexts.Count)
        {
            roleTexts[i].text += " (Level " + levels[i] + ")";
            i += 1;
        }
    }

}

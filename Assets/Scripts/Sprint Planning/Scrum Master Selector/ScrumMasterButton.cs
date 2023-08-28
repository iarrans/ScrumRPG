using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrumMasterButton : MonoBehaviour
{
    [SerializeField]
    private Text combatantName;

    [SerializeField]
    private Text currentRole;

    [SerializeField]
    private ScrumMasterButtonControl buttonControl;

    [SerializeField]
    private GameObject combatant;

    [SerializeField]
    private Image combatantSprite;

    [SerializeField]
    private Slider motivation;

    [SerializeField]
    private Slider energy;

    private string nameString;


    //setters
    public void SetName(string nombre)
    {
        combatantName.text = nombre;
    }

    public void SetCurrentRole(string role)
    {
        currentRole.text = role;
    }

    public void SetMotivation(float value)
    {
        motivation.value = value;
    }

    public void SetEnergy(float value)
    {
        energy.value = value;
    }

    public void SetCombatant(GameObject combatante)
    {
        combatant = combatante;
    }

    public void SetCombatantSprite(Sprite combatanteSprite)
    {
        combatantSprite.sprite = combatanteSprite;
    }

    //Método que llama a setear ScrumMaster.
    public void SetScrumMaster()
    {
        buttonControl.SetScrumMaster(combatant);
    }
}

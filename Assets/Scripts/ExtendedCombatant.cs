using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExtendedCombatant : MonoBehaviour
{
    public Sprite portrait;

    public int developerLevel = 1;
    public int qualityAssuranceLevel = 1;
    public int testerLevel = 1;
    public int UIDesignerLevel = 1;
    public int deploymentLevel = 1;

    public Dictionary<int, float> friendship;                       //Key: Combatant id; Value: Friendship value with that combatant

    Combatant combatant;

    private void Start()
    {
        combatant = ComponentHelper.GetCombatant(gameObject);
        UpdateClassLevels(0);
        UpdateClassLevels(1);
        UpdateClassLevels(2);
        UpdateClassLevels(3);
        UpdateClassLevels(4);
    }

    private void Update()
    {
        if (GetComponent<TopDown2DPlayerController>() != null) GetComponent<TopDown2DPlayerController>().enabled = false;
        UpdateProductivity();
    }

    public void UpdateProductivity()
    {
        int stability = combatant.Status[11].GetValue();
        int nikoValue = combatant.Status[13].GetValue();

        float productivityFloat;
        if (nikoValue >= 0)
        {
            productivityFloat = Mathf.Pow(((2f + 2f * nikoValue) / 2f), (100f - (stability / 2f)) / 100);
        }
        else
        {
            productivityFloat = Mathf.Pow((2f / (2f - 2f * nikoValue)), (100f - (stability / 2f)) / 100);
        }

        int classLevel = combatant.Class.Level;
        switch (classLevel)
        {
            case 1:
                productivityFloat = 0.66666f;
                break;
            case 2:
                productivityFloat = 0.83333f;
                break;
            case 3:
                productivityFloat = 1;
                break;
            case 4:
                productivityFloat = 1.25f;
                break;
            case 5:
                productivityFloat = 1.5f;
                break;
        }

        int productivity = Mathf.FloorToInt(productivityFloat * 100);
        combatant.Status[12].Set(productivity, productivity);
    }

    private void UpdateClassLevels(int classId)
    {
        int originalClassId = combatant.Class.ID; //Get the original class id

        combatant.Class.Change(classId, false, true, true, false, false, false, false, false);  //Switch to the class that we'll be leveling up

        int i = 0;  //Number of level ups

        switch (classId)    //Set number of level ups depending on the class that we're working on
        {
            case 0:
                i = Mathf.Max(1, developerLevel);   //An ability can't have its level be a number below 1
                break;
            case 1:
                i = Mathf.Max(1, qualityAssuranceLevel);
                break;
            case 2:
                i = Mathf.Max(1, testerLevel);
                break;
            case 3:
                i = Mathf.Max(1, UIDesignerLevel);
                break;
            case 4:
                i = Mathf.Max(1, deploymentLevel);
                break;
        }

        foreach (AbilityShortcut ability in combatant.Abilities.GetAll())   //Update role changing abilities level
        {
            if (ability.ID == classId + 7)
            {
                ability.Level = i;
                break;
            }
        }

        while (i > 1)   //For each level, perform level up
        {
            combatant.Class.ForceLevelUp();
            i -= 1;
        }

        combatant.Class.Change(originalClassId, false, true, true, false, false, false, false, false);  //Return to the original class
    }

    public float GetFriendship(int combatantId)
    {
        return friendship[combatantId];
    }

    public void SetFriendship(int combatantId, float newValue)
    {
        friendship[combatantId] = newValue;
        CheckMax(combatantId);
    }

    public void IncreaseFriendship(int combatantId, float increasedValue)
    {
        friendship[combatantId] += increasedValue;
        CheckMax(combatantId);
    }

    public void CheckMax(int combatantId)
    {
        if (friendship[combatantId] > 99)
        {
            friendship[combatantId] = 99;
        }
    }

    public void SetOpacity(float opacity)
    {
        Color color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, opacity);
    }

    public void BattlePositions(Vector3 arenaPosition)
    {
        Group combatantGroup = combatant.Group;
        int i = 0;
        foreach (Combatant c in combatantGroup.GetGroup())
        {
            c.GameObject.transform.position = new Vector3(arenaPosition.x + 2, arenaPosition.y + 3 - 1.6f * i, arenaPosition.z);
            i += 1;
        }
    }
}

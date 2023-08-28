using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ORKFramework;

public class CombatantUI : MonoBehaviour
{
    public Image battleSprite;

    public Image scrumMasterBackground;

    public Image stageIcon;
    public Image nikoNikoIcon;

    public Image combatantSprite;

    public Text combatantName;

    public Text combatantRole;
    public Text combatantSuperRole;
    public Image roleMatch;

    public Slider combatantMotivationSlider;
    public Slider combatantEnergySlider;

    public Slider combatantAccomplishmentSlider;

    Combatant combatant;
    GameObject UIPlayerGameObject;
    GameObject UIEnemyGameObject;
    Vector2 basePosition;
    RectTransform combatantUIReference;

    // Start is called before the first frame update
    void Start()
    {
        combatant = ComponentHelper.GetCombatant(gameObject);
        UIPlayerGameObject = combatantSprite.transform.parent.gameObject;
        UIEnemyGameObject = combatantAccomplishmentSlider.transform.parent.gameObject;
        basePosition = UIPlayerGameObject.GetComponent<RectTransform>().anchoredPosition;
        combatantUIReference = GameObject.FindGameObjectWithTag("CombatantUIReference").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float height = combatantUIReference.rect.height;
        Vector2 size = UIPlayerGameObject.GetComponent<RectTransform>().sizeDelta;
        UIPlayerGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, height / 5);
        size = UIPlayerGameObject.GetComponent<RectTransform>().sizeDelta;
        battleSprite.GetComponent<RectTransform>().sizeDelta = new Vector2(size.y, size.y);
        battleSprite.enabled = combatant.Battle.InBattle;
        battleSprite.sprite = GetComponent<SpriteRenderer>().sprite;

        UpdatePlayerUI();
    }

    public void DisplayPlayerUI(bool display)
    {
        if (display)
        {
            UpdatePlayerUI();
        }
        UIPlayerGameObject.SetActive(display);
        UIEnemyGameObject.SetActive(false);
    }

    void DisplayEnemyUI(bool display)
    {
        UIPlayerGameObject.SetActive(false);
        UIEnemyGameObject.SetActive(display);
    }

    void UpdatePlayerUI()
    {
        int idInGroup = combatant.Group.GetMemberIndex(combatant);

        Vector2 size = UIPlayerGameObject.GetComponent<RectTransform>().sizeDelta;
        Vector3 scale = UIPlayerGameObject.GetComponent<RectTransform>().localScale;
        UIPlayerGameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(basePosition.x, basePosition.y - size.y * scale.y * idInGroup);

        combatantSprite.sprite = GetComponent<ExtendedCombatant>().portrait;

        scrumMasterBackground.gameObject.SetActive(IsScrumMaster());

        stageIcon.GetComponent<StageIcon>().ChangeSpirte(GetCombatantStage());
        nikoNikoIcon.GetComponent<NikoNikoEntry>().ChangeSpirte(GetCombatantNikoNikoEntry());

        combatantName.text = combatant.GetName();

        combatantRole.text = combatant.Class.Current.GetName() + " (Level " + combatant.Class.Level + ")";
        combatantSuperRole.text = GetComponent<Superrol>().superrol;
        roleMatch.enabled = combatant.Class.Current.GetName() == combatantSuperRole.text;
        switch (combatant.Class.ID)
        {
            case 0:
                roleMatch.color = RoleTags.developerColor;
                break;
            case 1:
                roleMatch.color = RoleTags.qualityAssuranceColor;
                break;
            case 2:
                roleMatch.color = RoleTags.testerColor;
                break;
            case 3:
                roleMatch.color = RoleTags.UIDesignerColor;
                break;
            case 4:
                roleMatch.color = RoleTags.deploymentColor;
                break;
        }

        combatantMotivationSlider.value = (float)combatant.Status[1].GetDisplayValue() / (float)combatant.Status[0].GetDisplayValue();
        combatantEnergySlider.value = (float)combatant.Status[3].GetDisplayValue() / (float)combatant.Status[2].GetDisplayValue();
    }

    bool IsScrumMaster()
    {
        List<StatusEffect> statusEffects = combatant.Status.Effects.GetEffects();

        foreach (StatusEffect statusEffect in statusEffects)
        {
            if (statusEffect.ID == 2)
            {
                return true;
            }
        }
        return false;
    }

    int GetCombatantStage()
    {
        int r = 0;
        List<StatusEffect> statusEffects = combatant.Status.Effects.GetEffects();
        foreach (StatusEffect statusEffect in statusEffects)
        {
            bool b = true;
            switch (statusEffect.ID)
            {
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                default:
                    b = false;
                    break;
            }
            if (b)
            {
                r = statusEffect.ID - 2;
            }
        }
        return r;
    }

    int GetCombatantNikoNikoEntry()
    {
        return combatant.Status[13].GetValue();
    }

    void UpdateEnemyUI()
    {
        combatantAccomplishmentSlider.value = (float)combatant.Status[7].GetDisplayValue() / (float)combatant.Status[6].GetDisplayValue();
    }
}

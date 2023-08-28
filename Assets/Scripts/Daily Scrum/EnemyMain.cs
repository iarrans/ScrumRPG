using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ORKFramework;

public class EnemyMain : MonoBehaviour
{
    public Text accomplishmentMeter;
    public Text accomplishmentShade;
    int maxAccomplishment;
    int accomplishment;

    public Text accomplishmentChangeValue;
    public Text accomplishmentChangeValueShade;
    float popUpDamageTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxAccomplishment = ComponentHelper.GetCombatant(gameObject).Status[6].GetValue();
        ComponentHelper.GetCombatant(gameObject).Status[7].Set(maxAccomplishment, maxAccomplishment);
    }

    // Update is called once per frame
    void Update()
    {
        accomplishment = ComponentHelper.GetCombatant(gameObject).Status[7].GetValue();
        accomplishmentMeter.text = (100 - Mathf.RoundToInt((float)accomplishment / maxAccomplishment * 100)).ToString() + "%";
        accomplishmentShade.text = accomplishmentMeter.text;

        if (popUpDamageTime > 0)
        {
            popUpDamageTime -= Time.deltaTime;
            if (popUpDamageTime <= 0)
            {
                accomplishmentChangeValue.gameObject.SetActive(false);
                accomplishmentChangeValueShade.gameObject.SetActive(false);
            }
        }
    }

    public void DisplayDamage(float damage)
    {
        int maxDamage = Mathf.RoundToInt((float)accomplishment / maxAccomplishment * 100);
        damage = Mathf.Min(ConvertToPercentage(damage), maxDamage);
        accomplishmentChangeValue.gameObject.SetActive(true);
        accomplishmentChangeValueShade.gameObject.SetActive(true);
        accomplishmentChangeValue.text = "+" + damage + "%";
        accomplishmentChangeValueShade.text = "+" + damage + "%";
        popUpDamageTime = 1;
    }

    int ConvertToPercentage(float damage)
    {
        return Mathf.RoundToInt((- damage / maxAccomplishment) * 100);
    }
}

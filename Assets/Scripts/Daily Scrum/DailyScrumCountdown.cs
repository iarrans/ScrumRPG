using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyScrumCountdown : MonoBehaviour
{
    public float countdown = 60;
    float currentCountdown;
    public CountdownUI countdownUI;
    int timeOut = 0;
    bool freezeCountdown = false;
    public int numberOfTimesFrozen = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentCountdown = countdown;
    }

    // Update is called once per frame
    void Update()
    {
        countdownUI.dangerLevel = timeOut;
        if (ORK.Battle.InBattle)
        {
            countdownUI.gameObject.SetActive(true);
            if (!freezeCountdown)
            {
                currentCountdown -= Time.deltaTime;
                Vector3 rotation = countdownUI.rotatingObject.eulerAngles;
                countdownUI.rotatingObject.eulerAngles = new Vector3(rotation.x, rotation.y, currentCountdown * (360 / countdown));
                if (currentCountdown <= 0)
                {
                    SetVariable();
                    timeOut += 1;
                    ResetCountdown();
                }
            }
        }
        else
        {
            countdownUI.gameObject.SetActive(false);
        }
    }

    void ResetCountdown()
    {
        currentCountdown = countdown;
    }

    public void FreezeCountdown(bool freeze)
    {
        freezeCountdown = freeze;
    }

    public void LimitedFreezeCountdown(bool freeze)
    {
        if (numberOfTimesFrozen <= 0)
        {
            freezeCountdown = freeze;
            if (!freeze) numberOfTimesFrozen += 1;
        }
        else
        {
            freezeCountdown = false;
        }
    }

    public void NewTurn()
    {
        ResetCountdown();
        timeOut = 0;
        SetVariable();
    }

    float GetProductivityMultiplier()
    {
        return 0.5f + 0.5f / (timeOut + 1);
    }

    void SetVariable()
    {
        ORK.Game.Variables.Set("DailyPenalty", GetProductivityMultiplier());
    }
}

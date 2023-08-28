using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ORKFramework;

public class SprintSlider : MonoBehaviour
{
    Sprint sprint;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        sprint = GameObject.FindGameObjectWithTag("BattleMain").GetComponent<Sprint>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        int totalTurns = sprint.turns;
        int currentTurn = ORK.Battle.Turn;
        slider.value = (float)currentTurn / (totalTurns + 1);
    }
}

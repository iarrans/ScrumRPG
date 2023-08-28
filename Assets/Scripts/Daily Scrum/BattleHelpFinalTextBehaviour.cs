using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHelpFinalTextBehaviour : MonoBehaviour
{
    public GameObject ifFrozen;
    public GameObject ifNotFrozen;
    public DailyScrumCountdown countdown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        countdown = GameObject.FindGameObjectWithTag("BattleMain").GetComponent<DailyScrumCountdown>();
        int numberOfTimesFrozen = countdown.numberOfTimesFrozen;
        ifFrozen.SetActive(numberOfTimesFrozen <= 0);
        ifNotFrozen.SetActive(numberOfTimesFrozen > 0);
    }
}

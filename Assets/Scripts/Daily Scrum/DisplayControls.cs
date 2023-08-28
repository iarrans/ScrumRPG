using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class DisplayControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.SetActive(ORK.Battle.InBattle);
        transform.GetChild(1).gameObject.SetActive(!ORK.Battle.InBattle);
    }
}

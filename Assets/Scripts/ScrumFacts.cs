using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumFacts : MonoBehaviour
{
    public Transform scrumFacts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllScrumFacts()
    {
        int c = scrumFacts.childCount;
        while (c > 0)
        {
            c -= 1;
            scrumFacts.GetChild(c).gameObject.SetActive(false);
        }
    }
}

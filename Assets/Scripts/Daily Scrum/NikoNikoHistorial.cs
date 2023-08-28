using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikoNikoHistorial : MonoBehaviour
{
    public List<int> historialNikoNiko;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (historialNikoNiko.Count > NikoNikoCalendar.turnsToDisplay)
        {
            historialNikoNiko.RemoveAt(0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{

    public string missionname;

    public string desc;

    public bool missionflag;

    public double motivamount;

    public bool Equals(Mission mission)
    {
        return mission.missionname == this.missionname && mission.motivamount == this.motivamount && mission.desc == this.desc;
    }

    public static bool Equals(Mission mission1, Mission mission2)
    {
        return mission1.missionname == mission2.missionname && mission1.motivamount == mission2.motivamount && mission1.desc == mission2.desc;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

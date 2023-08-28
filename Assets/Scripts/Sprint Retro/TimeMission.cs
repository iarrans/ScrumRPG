using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMission : Mission
{
    public int duration;

    public TimeMission()
    {

    }

    public TimeMission(int duration, string missionname, string desc, bool missionflag, double motivamount)
    {
        this.duration = duration;
        this.missionname = missionname;
        this.desc = desc;
        this.missionflag = missionflag;
        this.motivamount = motivamount;
    }

}

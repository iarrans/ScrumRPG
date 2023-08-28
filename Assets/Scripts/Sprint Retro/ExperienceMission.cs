using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceMission : Mission
{
    public int expObjective;

    public ExperienceMission()
    {

    }

    public ExperienceMission(int expObjective, string missionname, string desc, bool missionflag, double motivamount)
    {
        this.expObjective = expObjective;
        this.missionname = missionname;
        this.desc = desc;
        this.missionflag = missionflag;
        this.motivamount = motivamount;
    }
}

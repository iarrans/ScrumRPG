using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDownEntry
{
    public int storyPoints;
    public int turn;

    public BurnDownEntry(int storyPoints, int turn)
    {
        this.storyPoints = storyPoints;
        this.turn = turn;
    }

    public string Print()
    {
        return "[" + storyPoints + ", " + turn + "]";
    }
}

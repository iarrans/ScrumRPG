using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;
using System.Linq;

public class Sprint : MonoBehaviour
{
    public int turns = 10;

    public int totalStoryPoints;

    public int currentStoryPoints;

    Project project;

    // Start is called before the first frame update
    void Start()
    {
        project = GameObject.FindGameObjectWithTag("Project").GetComponent<Project>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentStoryPoints();
        if (ORK.Battle.InBattle && ORK.Battle.Turn > turns)
        {
            Debug.Log("End battle");
            ORK.Battle.EndBattle(BattleOutcome.Victory);
        }
    }

    public void EmptySprint()
    {
        totalStoryPoints = 0;
        currentStoryPoints = 0;
        GetComponent<BurnDown>().EmptyEntries();
    }

    public int GetTotalStoryPoints()
    {
        totalStoryPoints = GetStoryPoints();
        return totalStoryPoints;
    }

    public int GetCurrentStoryPoints()
    {
        currentStoryPoints = GetStoryPoints();
        return currentStoryPoints;
    }

    public int GetStoryPoints()
    {
        int storyPoints = 0;
        List<List<int>> sprintBacklog = project.groupSprint;
        foreach (List<int> enemy in sprintBacklog)
        {
            storyPoints += enemy[1];
        }
        return storyPoints;
    }

    public void SetTurns(int turns)
    {
        this.turns = turns;
    }
}

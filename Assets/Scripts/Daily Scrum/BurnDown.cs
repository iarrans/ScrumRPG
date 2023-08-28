using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class BurnDown : MonoBehaviour
{
    List<BurnDownEntry> burnDownEntries = new List<BurnDownEntry>();
    Sprint sprint;

    // Start is called before the first frame update
    void Start()
    {
        sprint = GameObject.FindGameObjectWithTag("BattleMain").GetComponent<Sprint>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<BurnDownEntry> GetBurnDownEntries()
    {
        return burnDownEntries;
    }

    public void AddEntry()
    {
        if (ORK.Battle.InBattle)
        {
            int turn = ORK.Battle.Turn;
            if (burnDownEntries.Count == 0 || burnDownEntries[burnDownEntries.Count - 1].turn != turn)
            {
                List<Combatant> enemyCombatants = EnemyParty.GetEnemies();
                int storyPoints = sprint.currentStoryPoints;
                BurnDownEntry burnDownEntry = new BurnDownEntry(storyPoints, turn);
                burnDownEntries.Add(burnDownEntry);
            }
        }
    }

    public void EmptyEntries()
    {
        burnDownEntries = new List<BurnDownEntry>();
    }
}

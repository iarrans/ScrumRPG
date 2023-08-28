using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionList : MonoBehaviour
{
    MissionsEvaluations missionsEvaluations;
    public GameObject missionPrefab;
    public Transform content;
    public Scrollbar verticalScroll;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DisplayMissionList()
    {
        if (!ORK.Battle.InBattle)
        {
            GameObject[] grupoCombatants = GameObject.FindGameObjectsWithTag("GroupCombatant");
            foreach (GameObject member in grupoCombatants)
            {
                member.GetComponent<CombatantUI>().DisplayPlayerUI(false);
            }
        }
        DeleteMissions();

        missionsEvaluations = GameObject.FindGameObjectWithTag("Project").GetComponent<MissionsEvaluations>();
        List<Mission> missions = missionsEvaluations.missions;
        int i = 0;
        foreach (Mission mission in missions)
        {
            GameObject missionObject = Instantiate(missionPrefab, content);
            missionObject.GetComponent<MissionPanel>().missionName.text = mission.missionname;
            missionObject.GetComponent<MissionPanel>().missionDescripction.text = mission.desc;
            missionObject.GetComponent<MissionPanel>().tick.enabled = false;
            missionObject.GetComponent<MissionPanel>().motivation.text = (mission.motivamount * 100).ToString() + "%";
            Vector2 position = missionObject.GetComponent<RectTransform>().anchoredPosition;

            Vector2 size = missionObject.GetComponent<RectTransform>().sizeDelta;
            if (ORK.Battle.InBattle)
            {
                missionObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x * 0.5f, size.y * 0.666666666f);
                size = missionObject.GetComponent<RectTransform>().sizeDelta;
            }
            missionObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, -size.y * i);

            i += 1;

            Vector2 contentSize = content.GetComponent<RectTransform>().sizeDelta;
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(contentSize.x, size.y * i);
        }

        verticalScroll.value = 1;
    }

    void DeleteMissions()
    {
        int i = content.childCount;
        while (i > 0)
        {
            i -= 1;
            Destroy(content.GetChild(i).gameObject);
        }
    }
}

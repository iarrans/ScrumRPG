using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NikoNikoCalendar : MonoBehaviour
{
    public Transform header;
    public GameObject turnNumber;
    public GameObject memberEntry;
    public Transform rowHolder;
    public RectTransform membersText;

    bool hasIncreasedThisTurn;
    public int number;

    bool calendarOpen = false;

    public static CanvasScaler canvasScaler;
    public static float initialPos;
    public static float offset;
    public static int turnsToDisplay = 14;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseNumber()
    {
        if (!hasIncreasedThisTurn)
        {
            hasIncreasedThisTurn = true;
            number += 1;
        }
    }

    public void EndTurn()
    {
        hasIncreasedThisTurn = false;    
    }

    public void ToggleCalendar()
    {
        if (calendarOpen)
        {
            CloseCalendar();
        }
        else
        {
            OpenCalendar();
        }
    }

    void OpenCalendar()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        Vector2 panelSize = new Vector2(canvasScaler.referenceResolution.x, canvasScaler.referenceResolution.y) * 0.9f;
        initialPos = membersText.sizeDelta.x + membersText.anchoredPosition.x;
        offset = (panelSize.x - initialPos) / (turnsToDisplay + 2);

        DeleteObjects(header, 1);
        int i = 0;
        while (i < turnsToDisplay)
        {
            GameObject text = GenerateObject(turnNumber, i, header);
            i += 1;
            int textNumber = i + Mathf.Max(0, number - turnsToDisplay);
            text.GetComponent<Text>().text = textNumber.ToString();
        }

        calendarOpen = true;
        transform.GetChild(1).gameObject.SetActive(true);
        GameObject[] party = GameObject.FindGameObjectsWithTag("GroupCombatant");
        i = 0;
        foreach (GameObject member in party)
        {
            GameObject entry = Instantiate(memberEntry, rowHolder);
            Vector2 position = entry.GetComponent<RectTransform>().anchoredPosition;
            Vector2 entrySize = entry.GetComponent<RectTransform>().sizeDelta;
            entry.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y - entrySize.y * i);
            i += 1;
            entry.GetComponent<NikoNikoMemberEntry>().member = member;
            entry.GetComponent<NikoNikoMemberEntry>().UpdateMemberEntry();
        }
    }

    public static void DeleteObjects(Transform parent)
    {
        DeleteObjects(parent, 0);
    }

    public static void DeleteObjects(Transform parent, int skip)
    {
        int c = parent.childCount;
        while (c > skip)
        {
            Destroy(parent.GetChild(c - 1).gameObject);
            c -= 1;
        }
    }

    public static GameObject GenerateObject(GameObject prefab, int counter, Transform parent)
    {
        GameObject r = Instantiate(prefab, parent);
        Vector2 position = prefab.GetComponent<RectTransform>().anchoredPosition;
        r.GetComponent<RectTransform>().anchoredPosition = new Vector2(initialPos + offset * counter, position.y);
        return r;
    }

    void CloseCalendar()
    {
        calendarOpen = false;
        transform.GetChild(1).gameObject.SetActive(false);
        int children = rowHolder.childCount;
        while (children > 0)
        {
            Destroy(rowHolder.GetChild(children - 1).gameObject);
            children -= 1;
        }
    }
}

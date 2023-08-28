using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ORKFramework;

public class BurnDownChart : MonoBehaviour
{
    public GameObject node;
    public RectTransform bottomReferenceNode;
    public RectTransform topReferenceNode;

    public GameObject battleMain;

    bool displayBurnDown = false;

    int childCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        childCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BurnDownButton()
    {
        if (displayBurnDown)
        {
            displayBurnDown = false;
            EmptyEntries();
        }
        else
        {
            Sprint sprint = battleMain.GetComponent<Sprint>();
            BurnDown burnDown = battleMain.GetComponent<BurnDown>();
            displayBurnDown = true;
            DrawEntries(burnDown.GetBurnDownEntries(), sprint);
        }
        transform.parent.parent.gameObject.SetActive(displayBurnDown);
    }

    void DrawEntries(List<BurnDownEntry> burnDownEntries, Sprint sprint)
    {
        Vector2 topReferencePosition = topReferenceNode.position;
        Vector2 bottomReferencePosition = bottomReferenceNode.position;

        Debug.Log(burnDownEntries.Count);
        foreach (BurnDownEntry entry in burnDownEntries)
        {
            GameObject entryGameObject = Instantiate(node, transform);
            float x = topReferencePosition.x - (topReferencePosition.x - bottomReferencePosition.x) * (((float)entry.turn - 1) / sprint.turns);
            float y = topReferencePosition.y - (topReferencePosition.y - bottomReferencePosition.y) * (1 - ((float)entry.storyPoints) / sprint.totalStoryPoints);

            entryGameObject.GetComponent<RectTransform>().position = new Vector2(x, y);
        }
    }

    void EmptyEntries()
    {
        int children = transform.childCount;
        int i = childCount;
        while (i < children)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Destroy(child);
            i += 1;
        }
    }
}

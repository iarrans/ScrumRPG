using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;
using UnityEngine.UI;

public class BattleUIBehaviour : MonoBehaviour
{
    public bool hideChildren = false;

    Image image;
    Button button;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        if (transform.childCount > 0) text = transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hideChildren)
        {
            int c = transform.childCount;
            while (c > 0)
            {
                transform.GetChild(0).gameObject.SetActive(ORK.Battle.InBattle);
                c -= 1;
            }
        }
        else
        {
            if (image != null) image.enabled = ORK.Battle.InBattle;
            if (button != null) button.enabled = ORK.Battle.InBattle;
            if (text != null) text.enabled = ORK.Battle.InBattle;
        }
    }
}

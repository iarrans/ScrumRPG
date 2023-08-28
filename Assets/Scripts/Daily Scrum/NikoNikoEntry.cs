using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NikoNikoEntry : MonoBehaviour
{
    public Sprite veryHappyIcon;
    public Sprite happyIcon;
    public Sprite normalIcon;
    public Sprite sadIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSpirte(int nikoNiko)
    {
        switch (nikoNiko)
        {
            case -1:
                GetComponent<Image>().sprite = sadIcon;
                break;
            case 0:
                GetComponent<Image>().sprite = normalIcon;
                break;
            case 1:
                GetComponent<Image>().sprite = happyIcon;
                break;
            case 2:
                GetComponent<Image>().sprite = veryHappyIcon;
                break;
            default:
                break;
        }
    }
}

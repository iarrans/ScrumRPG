using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageIcon : MonoBehaviour
{
    public Sprite stage1;
    public Sprite stage2;
    public Sprite stage3;
    public Sprite stage4;
    public Sprite stage5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeSpirte(int stage)
    {
        switch (stage)
        {
            case 1:
                GetComponent<Image>().sprite = stage1;
                break;
            case 2:
                GetComponent<Image>().sprite = stage2;
                break;
            case 3:
                GetComponent<Image>().sprite = stage3;
                break;
            case 4:
                GetComponent<Image>().sprite = stage4;
                break;
            case 5:
                GetComponent<Image>().sprite = stage5;
                break;
            default:
                break;
        }
    }
}

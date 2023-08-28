using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour
{
    public RectTransform rotatingObject;
    public Image colorChangingObject;
    public int dangerLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        colorChangingObject.color = new Color(0 + 1 - Mathf.Pow(0.5f, dangerLevel), 0, 0, 1);
    }
}

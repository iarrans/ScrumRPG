using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasResolutionUpdate : MonoBehaviour
{
    CanvasScaler canvasScaler;

    // Start is called before the first frame update
    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        int height = Screen.height;
        int width = Screen.width;

        if (Mathf.Max(height, width) / Mathf.Min(height, width) < 2)
        {
            canvasScaler.referenceResolution = new Vector2(1600, 900);
        }
        else
        {
            canvasScaler.referenceResolution = new Vector2(1800, 900);
        }
    }
}

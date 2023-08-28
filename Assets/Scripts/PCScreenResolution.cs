using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCScreenResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            Screen.SetResolution(2160, 1080, true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

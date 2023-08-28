using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleTag : MonoBehaviour
{
    [SerializeField]
    RoleTags.Tag role;

    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        switch (role)
        {
            case RoleTags.Tag.Developer:
                image.color = RoleTags.developerColor;
                break;
            case RoleTags.Tag.QualityAssurance:
                image.color = RoleTags.qualityAssuranceColor;
                break;
            case RoleTags.Tag.Tester:
                image.color = RoleTags.testerColor;
                break;
            case RoleTags.Tag.UIDesigner:
                image.color = RoleTags.UIDesignerColor;
                break;
            case RoleTags.Tag.Deployment:
                image.color = RoleTags.deploymentColor;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

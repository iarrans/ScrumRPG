using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTags
{
    public enum Tag
    {
        Developer, QualityAssurance, Tester, UIDesigner, Deployment
    }

    public static Color developerColor = new Color(0.6f, 0, 0.6f);
    public static Color qualityAssuranceColor = new Color(0.6f, 0.6f, 0);
    public static Color testerColor = new Color(0, 0.5f, 0.15f);
    public static Color UIDesignerColor = new Color(0.3f, 0, 0.6f);
    public static Color deploymentColor = new Color(0, 0.15f, 0.6f);
}

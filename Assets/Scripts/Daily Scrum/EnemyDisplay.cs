using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class EnemyDisplay : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int level;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        level = ComponentHelper.GetCombatant(gameObject).Status.Level;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sortingOrder = 1000 - Mathf.FloorToInt(transform.position.y * 100);
        float newScale = 1 + 0.02f * level;
        transform.localScale = new Vector3(newScale, newScale);
    }
}

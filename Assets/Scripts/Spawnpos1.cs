using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ORKFramework;

public class Spawnpos1 : MonoBehaviour
{

    public Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnCharacter()
    {

        transform.position = spawnPosition;
        
    }

    public void SetSpawnPosition(Vector3 posicion)
    {
        this.spawnPosition = posicion;
        
    }

    public void ChangePosition(Vector3 posicion)
    {
        this.gameObject.transform.position = posicion;
    }
}

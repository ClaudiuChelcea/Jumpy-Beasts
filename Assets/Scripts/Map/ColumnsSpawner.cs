using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnsSpawner : MonoBehaviour
{
    // Variables
    public float timeBeforeSpawn = 1f; // time interval before columns start spawning
    public float timeBetweenSpawns = 2f; // time between each columns
    private float beforeSpawnTimer = 0f; // becomes between spawn timer after the first column is spawned
    public GameObject column_pair;
    public float height = 0f;
    public float time_to_disappear = 5f; // time for pair of columns to be destroyed

    // Update is called once per frame
    void Update()
    {


        if (beforeSpawnTimer > timeBeforeSpawn)
        {
            GameObject pipe = Instantiate(column_pair);
            pipe.transform.position = new Vector2(6, 4);
            pipe.transform.position = pipe.transform.position + new Vector3(0, Random.RandomRange(-height, height), 0);
            pipe.transform.parent = this.transform;
            Destroy(pipe, time_to_disappear); // destroy the instantiated pipe after time_to_disappear seconds
            beforeSpawnTimer -= timeBetweenSpawns;
        } // else don't do anything
        beforeSpawnTimer += Time.deltaTime;
    }
}

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
    public Vector2 columnHeightInterval;
    // Update is called once per frame
    void Update()
    {


        if (beforeSpawnTimer > timeBeforeSpawn)
        {
            GameObject pipe = Instantiate(column_pair);
            pipe.transform.position = new Vector2(6, 4); // put them right near the map
            pipe.transform.position = new Vector3(pipe.transform.position.x, Random.Range(columnHeightInterval.x, columnHeightInterval.y), pipe.transform.position.z);
            pipe.transform.parent = this.transform;
            Destroy(pipe, time_to_disappear); // destroy the instantiated pipe after time_to_disappear seconds
            beforeSpawnTimer -= timeBetweenSpawns;
        } // else don't do anything
        beforeSpawnTimer += Time.deltaTime;
    }
}

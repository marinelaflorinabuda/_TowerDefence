using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{
    public EntitySpawnerAuthoring entitySpawnerAuthoring;
    public int numberOfEntitiesToSpawn;

    public float minDistanceOfSpawn;
    public float maxDistanceOfSpawn;

    void Start()
    {
        Vector3[] spawnPositions = new Vector3[numberOfEntitiesToSpawn];

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            float randomAngle = UnityEngine.Random.Range(0, Mathf.PI * 2);
            float randomDistance = UnityEngine.Random.Range(minDistanceOfSpawn, maxDistanceOfSpawn);
            Vector3 spawnPosition = new Vector3(randomDistance*Mathf.Sin(randomAngle),0,randomDistance*Mathf.Cos
            (randomAngle));

            spawnPositions[i] = spawnPosition;
        }
        
        entitySpawnerAuthoring.CreateEnemyAt(spawnPositions);

    }

}

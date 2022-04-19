using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Wave[] waves;


    Wave currentWave;
    int currentWaveNumber;
    int prefabsRemainingToSpawn;
    float nextSpawnTime;

    [System.Serializable]
    public class Wave
    {
        public int prefabCount;
        public float timeBetweenSpawns;
    }

    void NextWave()
    {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];

        prefabsRemainingToSpawn = currentWave.prefabCount;
    }

    private void SpawnMethod()
    {
        if (prefabsRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            prefabsRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            //Instantiate the object:  Controller control = Instantiate(prefab, Vector2.zero, Quaternion.identity) as Controller;
        }
    }
}

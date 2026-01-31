using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject[] obstaclePrefabs;   
    public float laneDistance = 3f;       
    public float spawnChance = 0.5f;     

    [Header("Timing")]
    public float initialDelay = 5f;       
    public float spawnInterval = 2f;   

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        // --- Odotetaan alussa ---
        yield return new WaitForSeconds(initialDelay);

        // --- Jatkuva spawn ---
        while (true)
        {
            if (obstaclePrefabs.Length > 0 && Random.value <= spawnChance)
            {
                int lane = Random.Range(0, 3);
                float xPos = (lane - 1) * laneDistance;

                Vector3 spawnPos = transform.position;
                spawnPos.x = xPos;
                spawnPos.y += 0.5f;

                int index = Random.Range(0, obstaclePrefabs.Length);
                Instantiate(obstaclePrefabs[index], spawnPos, Quaternion.identity, transform);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

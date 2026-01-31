using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float laneDistance = 3f;
    public float spawnChance = 0.5f;

    void Start()
    {
        if (obstaclePrefabs.Length == 0) return;
        if (Random.value > spawnChance) return;

        int lane = Random.Range(0, 3);
        float xPos = (lane - 1) * laneDistance;

        Vector3 spawnPos = transform.position;
        spawnPos.x = xPos;
        spawnPos.y += 0.5f;

        int index = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[index], spawnPos, Quaternion.identity, transform);
    }
}

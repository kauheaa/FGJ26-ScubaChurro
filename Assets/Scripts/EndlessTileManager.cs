using System.Collections.Generic;
using UnityEngine;

public class EndlessTileManager : MonoBehaviour
{
    public Transform player;
    public GameObject[] tilePrefabs;
    public ObstacleSpawner obstacleSpawner; 

    public int tilesOnScreen = 6;

    private float nextSpawnZ = 0f;
    private Queue<GameObject> activeTiles = new Queue<GameObject>();
    private int tilesSpawned = 0;

    void Start()
    {
        if (tilePrefabs.Length == 0) return;

        for (int i = 0; i < tilesOnScreen; i++)
            SpawnTile();
    }
void Update()
{
    // Spawnataan uutta kun pelaaja lähestyy loppua
    if (player.position.z >= nextSpawnZ - 250f)
        SpawnTile();

   
    if (activeTiles.Count > 2)
    {
        GameObject oldestTile = activeTiles.Peek();

        
        Renderer r = oldestTile.GetComponentInChildren<Renderer>();
        if (r == null) return;

        float tileEndZ = r.bounds.max.z;

        
        float safeDistance = 30f; 

        if (player.position.z > tileEndZ + safeDistance)
        {
            Destroy(activeTiles.Dequeue());
        }
    }
}


    private void SpawnTile()
    {
        GameObject prefab;

        if (tilesSpawned < 10)
            prefab = tilePrefabs[0];
        else
            prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];

        GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        Renderer[] renderers = tile.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(tile.transform.position, Vector3.zero);
        foreach (Renderer r in renderers)
            bounds.Encapsulate(r.bounds);

        float tileLengthZ = bounds.size.z;

        float offset = bounds.min.z - tile.transform.position.z;
        tile.transform.position = new Vector3(0f, 0f, nextSpawnZ - offset);

        activeTiles.Enqueue(tile);
        nextSpawnZ += tileLengthZ;
        tilesSpawned++;

        if (obstacleSpawner != null && tilesSpawned > 10)
           obstacleSpawner.SpawnObstacle(tile.transform.position, tile);
    }
}

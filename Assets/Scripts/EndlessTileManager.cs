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
    if (player.position.z >= nextSpawnZ - 60f)
        SpawnTile();

    // Poistetaan tile VASTA kun pelaajan takana on vähintään 2 tileä
    if (activeTiles.Count > 2)
    {
        GameObject oldestTile = activeTiles.Peek();

        // Haetaan vanhimman tilen renderer
        Renderer r = oldestTile.GetComponentInChildren<Renderer>();
        if (r == null) return;

        float tileEndZ = r.bounds.max.z;

        // Pelaajan pitää olla SELVÄSTI kahden tilen päässä
        float safeDistance = 30f; // ← säädä tarvittaessa

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

        // Instantiate prefab
        GameObject tile = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        // Bounds
        Renderer[] renderers = tile.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(tile.transform.position, Vector3.zero);
        foreach (Renderer r in renderers)
            bounds.Encapsulate(r.bounds);

        float tileLengthZ = bounds.size.z;

        // Siirrä tile niin, että alku (min Z) on nextSpawnZ
        float offset = bounds.min.z - tile.transform.position.z;
        tile.transform.position = new Vector3(0f, 0f, nextSpawnZ - offset);

        activeTiles.Enqueue(tile);
        nextSpawnZ += tileLengthZ;
        tilesSpawned++;

        if (obstacleSpawner != null && tilesSpawned > 10)
            obstacleSpawner.SpawnObstacle(tile.transform.position);
    }
}

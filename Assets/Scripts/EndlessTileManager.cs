
using System.Collections.Generic;
using UnityEngine;

public class EndlessTileManager : MonoBehaviour
{
    public Transform player;
    public GameObject[] tilePrefabs;

    public int tilesOnScreen = 6;
    public float tileLength = 1f; 

    private float nextSpawnZ = 0f;
    private Queue<GameObject> activeTiles = new Queue<GameObject>();

    void Start()
    {
        if (tilePrefabs.Length == 0)
        {
            return;
        }

        tileLength = tilePrefabs[0].GetComponent<BoxCollider>().size.z;

        for (int i = 0; i < tilesOnScreen; i++)
            SpawnTile();
    }

    void Update()
    {
        if (player.position.z >= nextSpawnZ - (tilesOnScreen * tileLength))
            SpawnTile();

        if (activeTiles.Count > 0)
        {
            GameObject oldestTile = activeTiles.Peek();
            if (player.position.z - oldestTile.transform.position.z >= tileLength * 2f) 
            {
                Destroy(activeTiles.Dequeue());
            }
        }
    }

    private void SpawnTile()
    {
        int index = Random.Range(0, tilePrefabs.Length);
        GameObject prefab = tilePrefabs[index];

        Vector3 spawnPos = new Vector3(0f, 0f, nextSpawnZ + tileLength * 0.5f);
        GameObject tile = Instantiate(prefab, spawnPos, Quaternion.identity);

        activeTiles.Enqueue(tile);
        nextSpawnZ += tileLength;
    }
}

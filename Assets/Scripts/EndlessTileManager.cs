
using System.Collections.Generic;
using UnityEngine;

public class EndlessTileManager : MonoBehaviour
{
    public Transform player;
    public GameObject[] tilePrefabs;

    public int tilesOnScreen = 6;
    public float tileLength = 1f; // Kaikki prefabit oletetaan saman Z:n

    private float nextSpawnZ = 0f;
    private Queue<GameObject> activeTiles = new Queue<GameObject>();

    void Start()
    {
        if (tilePrefabs.Length == 0)
        {
            Debug.LogError("Ei tile-prefabeja!");
            return;
        }

        tileLength = tilePrefabs[0].GetComponent<BoxCollider>().size.z;

        for (int i = 0; i < tilesOnScreen; i++)
            SpawnTile();
    }

    void Update()
    {
        // Spawnataan uusi tile
        if (player.position.z >= nextSpawnZ - (tilesOnScreen * tileLength))
            SpawnTile();

        // Poistetaan vanha tile vain, jos se on tarpeeksi kaukana pelaajasta
        if (activeTiles.Count > 0)
        {
            GameObject oldestTile = activeTiles.Peek();
            if (player.position.z - oldestTile.transform.position.z >= tileLength * 2f) // takana 2 tileä
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

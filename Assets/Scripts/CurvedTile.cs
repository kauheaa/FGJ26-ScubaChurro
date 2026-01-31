using UnityEngine;

public class CurvedTile : MonoBehaviour
{
    public Transform childObject; // assign editorissa se child, jonka snapataan

    private bool playerOnTile = false;
    private bool hasSnapped = false;

    void Update()
    {
        CheckPlayerOnTile();

        // Snapataan vain kerran
        if (playerOnTile && !hasSnapped && childObject != null)
        {
            // Snapataan sijainti X=0, Y=0, Z=0
            childObject.position = new Vector3(0f, 0f, 0f);

            // Snapataan rotaatio Y=0
            Vector3 euler = childObject.eulerAngles;
            euler.y = 0f;
            childObject.eulerAngles = euler;

            hasSnapped = true;

            Debug.Log($"{childObject.name} snapped to position (0,0,0) and Y rotation 0");
        }
    }

    private void CheckPlayerOnTile()
    {
        Collider[] hits = Physics.OverlapBox(transform.position + Vector3.up * 0.5f, new Vector3(0.5f, 0.1f, 0.5f));

        playerOnTile = false;
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerOnTile = true;
                break;
            }
        }
    }
}

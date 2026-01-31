using UnityEngine;

public class CurvedTile : MonoBehaviour
{
    public Transform childObject; 

    private bool playerOnTile = false;
    private bool hasSnapped = false;

    void Update()
    {
        CheckPlayerOnTile();

       
        if (playerOnTile && !hasSnapped && childObject != null)
        {
            
            childObject.position = new Vector3(0f, 0f, 0f);

            
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

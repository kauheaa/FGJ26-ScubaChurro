using UnityEngine;

public class CurvedTile : MonoBehaviour
{
    public Transform childObject;    
    public float rotateDuration = 0.5f;

    private bool playerOnTile = false;
    private bool hasSnapped = false;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float rotateTimer = 0f;

    void Start()
    {
        if (childObject != null)
        {
            startRotation = childObject.rotation;
            targetRotation = Quaternion.Euler(
                childObject.eulerAngles.x,
                0f,
                childObject.eulerAngles.z
            );
        }
    }

    void Update()
    {
        CheckPlayerOnTile();

        if (playerOnTile && !hasSnapped && childObject != null)
        {
            rotateTimer += Time.deltaTime;
            float t = rotateTimer / rotateDuration;

            childObject.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            if (t >= 1f)
            {
                childObject.rotation = targetRotation;
                hasSnapped = true;
            }
        }
    }

    private void CheckPlayerOnTile()
    {
        Collider[] hits = Physics.OverlapBox(
            transform.position + Vector3.up * 0.5f,
            new Vector3(0.5f, 0.1f, 0.5f)
        );

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

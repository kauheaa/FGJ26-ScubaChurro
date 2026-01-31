using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 5, -10);

    [Header("Smooth")]
    public float smoothSpeed = 10f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            offset.y,
            target.position.z + offset.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            1f / smoothSpeed
        );
    }
}

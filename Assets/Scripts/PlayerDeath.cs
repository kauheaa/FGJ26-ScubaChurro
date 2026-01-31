using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private bool dead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (dead) return;

        if (other.CompareTag("Obstacle"))
        {
            dead = true;
            Time.timeScale = 0f;
        }
    }
}

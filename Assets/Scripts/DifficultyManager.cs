using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public PlayerMovement player;

    public float startSpeed = 10f;
    public float maxSpeed = 25f;
    public float speedIncreasePerSecond = 0.3f;

    void Start()
    {
        player.forwardSpeed = startSpeed;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        player.forwardSpeed += speedIncreasePerSecond * Time.deltaTime;
        player.forwardSpeed = Mathf.Min(player.forwardSpeed, maxSpeed);
    }
}

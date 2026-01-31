using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayButton : MonoBehaviour
{
    public void ReplayGame()
    {
        Time.timeScale = 1f; // TÄRKEÄ: palautetaan aika
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
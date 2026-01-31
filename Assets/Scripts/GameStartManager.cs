using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startPanel;   // Paneeli, jossa Play-nappi
    public Button playButton;       // Play-nappi
    public TMP_Text timerText;      // Timer UI
    public float startDelay = 2f;   // Animaation kesto

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;               // Pys‰ytet‰‰n peli aluksi
        timerText.gameObject.SetActive(false); // Piilotetaan Timer
    }

    public void OnPlayButtonPressed()
    {
        playButton.interactable = false;   // estet‰‰n uudelleen painaminen
        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(startDelay); // odotetaan animaation ajan

        startPanel.SetActive(false);       // piilotetaan paneeli
        timerText.gameObject.SetActive(true); // n‰ytet‰‰n timer

        Time.timeScale = 1f;               // peli alkaa
        gameStarted = true;
    }

    public bool GameHasStarted()
    {
        return gameStarted;
    }
}
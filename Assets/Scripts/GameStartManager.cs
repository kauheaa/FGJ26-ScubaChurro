using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StartScreenManager : MonoBehaviour
{
    public GameObject startPanel;   
    public Button playButton;       
    public TMP_Text timerText;     
    public float startDelay = 2f;   // Animaation kesto

    private bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;               
        timerText.gameObject.SetActive(false);
    }

    public void OnPlayButtonPressed()
    {
        playButton.interactable = false;  
        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(startDelay);

        startPanel.SetActive(false);      
        timerText.gameObject.SetActive(true); 

        Time.timeScale = 1f;
        gameStarted = true;
    }

    public bool GameHasStarted()
    {
        return gameStarted;
    }
}
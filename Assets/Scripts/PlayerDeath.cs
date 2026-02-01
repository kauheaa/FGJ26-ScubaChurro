using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    [Header("Gameplay")]
    public TimerTMP timer;         
    private bool dead = false;

    [Header("UI Elements")]
    public GameObject panel;        
    public TMP_Text highscoreText;  
    public TMP_Text scoreText;   
    public SimpleAudio audio;    

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dead) return;

        if (other.CompareTag("Obstacle"))
        {
            
            // animator.SetTrigger("Death");
            dead = true;
            audio.PlaySFX5();
            Time.timeScale = 0f;

            if (timer != null)
                timer.StopTimer();

            StartCoroutine(ShowEndPanelCoroutine());
        }
    }

    private IEnumerator ShowEndPanelCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);

        if (panel != null)
            panel.SetActive(true);

        float currentScore = timer != null ? timer.ElapsedTime : 0f;

        float highscore = PlayerPrefs.GetFloat("Highscore", 0f);

        if (currentScore > highscore)
        {
            highscore = currentScore;
            PlayerPrefs.SetFloat("Highscore", highscore);
            PlayerPrefs.Save();
        }

        if (highscoreText != null)
            highscoreText.text = FormatTime(highscore);

        if (scoreText != null)
            scoreText.text = FormatTime(currentScore);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}

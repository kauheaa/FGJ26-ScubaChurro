using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class EndPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;       
    public TMP_Text highscoreText; 
    public TMP_Text scoreText;     

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void ShowEndPanel(int currentScore)
    {
        StartCoroutine(ShowPanelCoroutine(currentScore));
    }

    private IEnumerator ShowPanelCoroutine(int currentScore)
    {
        yield return new WaitForSeconds(2f); 

        if (panel != null)
            panel.SetActive(true);

        int highscore = PlayerPrefs.GetInt("Highscore", 0);

        if (currentScore > highscore)
        {
            highscore = currentScore;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
        }

        if (highscoreText != null)
            highscoreText.text = "Highscore: " + highscore;

        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;
    }
}

using UnityEngine;
using TMPro;

public class TimerTMP : MonoBehaviour
{
    public TMP_Text timerText;      // TMP_Text UI-elementti
    private float elapsedTime;      // Kulunut aika sekunneissa
    private bool running = true;

    void Update()
    {
        if (!running) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    public void StopTimer()
    {
        running = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerUI();
        running = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    private bool isRunning = true;
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime * 5;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }
    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }
}

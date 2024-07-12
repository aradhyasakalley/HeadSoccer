using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] TextMeshProUGUI halfTimeText;
    float elapsedTime;
    private bool isRunning = true;

    // Delegate and event for game time over
    public delegate void GameTimeOverHandler();
    public static event GameTimeOverHandler OnGameTimeOver;

    // Delegate and event for half-time
    public delegate void HalfTimeHandler();
    public static event HalfTimeHandler OnHalfTime;

    void Update()
    {
        if (isRunning)
        {
            // *100 for faster timer 
            elapsedTime += Time.deltaTime * 50;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Check if the timer has reached 45:00 for half-time
            /*if (minutes == 45 && seconds == 0)
            {
                isRunning = false; // Stop the timer
                TriggerHalfTimeEvent();
            }*/

            // Check if the timer has reached 90:00 for full-time
            if (minutes == 90)
            {
                isRunning = false; // Stop the timer
                TriggerGameTimeOverEvent();
            }
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

    void TriggerGameTimeOverEvent()
    {
        if (OnGameTimeOver != null)
        {
            OnGameTimeOver.Invoke();
        }
    }

    void TriggerHalfTimeEvent()
    {
        if (OnHalfTime != null)
        {
            OnHalfTime.Invoke();
        }
    }
}

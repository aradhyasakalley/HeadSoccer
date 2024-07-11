using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI winnerText;
    float elapsedTime;
    private bool isRunning = true;

    // Delegate and event for game time over
    public delegate void GameTimeOverHandler();
    public static event GameTimeOverHandler OnGameTimeOver;

    void Update()
    {
        if (isRunning)
        {
            // *100 for faster timer 
            elapsedTime += Time.deltaTime * 50;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Check if the timer has reached 90:00
            if (minutes == 90)
            {
                isRunning = false; // Stop the timer
                //DisplayWinnerText();
                TriggerGameTimeOverEvent();
            }
        }
    }

    /*void DisplayWinnerText()
    {
        winnerText.text = "Winner!";
        winnerText.gameObject.SetActive(true);
    }*/

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
}

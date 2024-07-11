using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuCtrl : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

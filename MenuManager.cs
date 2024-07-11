using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void ViewControls()
    {
        SceneManager.LoadSceneAsync(1);
    }
}

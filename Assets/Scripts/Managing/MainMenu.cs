
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Handles interactions and navigation within the main menu.
/// </summary>
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

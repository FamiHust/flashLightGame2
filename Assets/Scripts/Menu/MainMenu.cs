using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SelectSceneMenu()
    {
        SceneManager.LoadScene(1);
    }

    // public void SettingMenu()
    // {

    // }

    public void QuitGame()
    {
        Application.Quit();
    }
}

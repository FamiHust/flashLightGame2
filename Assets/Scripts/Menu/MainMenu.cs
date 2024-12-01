using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMainMenu : MonoBehaviour
{
    public GameObject AskPanel;

    public void OpenAskMenu()
    {
        AskPanel.SetActive(true);
    }

    public void CloseAskMenu()
    {
        AskPanel.SetActive(false);
    }

    public void SelectSceneMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

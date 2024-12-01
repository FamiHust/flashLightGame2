using UnityEngine;

public class GamePauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}

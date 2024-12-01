using UnityEngine;

public class GameHintMenu : MonoBehaviour
{
    public GameObject AboutPanel;
    public GameObject HintPanel;

    public void Pause()
    {
        AboutPanel.SetActive(true);
    }

    public void Resume()
    {
        AboutPanel.SetActive(false);
    }

    public void PauseHint()
    {
        HintPanel.SetActive(true);
    }

    public void ResumeHint()
    {
        HintPanel.SetActive(false);
    }
    
}

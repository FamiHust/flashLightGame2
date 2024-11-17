using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    //[SerializeField] private int nextLevel;
    [SerializeField] private int replayLevel;
    private bool isPaused = false;
    public GameObject OverPanel;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
            GameOver();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // public void PauseTimer()
    // {
    //     isPaused = true;
    // }
    // public void ResumeTimer()
    // {
    //     isPaused = false;
    // }

    public void GameOver()
    {
        Time.timeScale = 0;
        OverPanel.SetActive(true);
    }

    public void ReplayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_" + replayLevel.ToString());
    }

    // public void NextLevel()
    // {
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene("Level_" + nextLevel.ToString());
    // }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Phương thức mới để thêm thời gian
    public void AddTime(float timeToAdd)
    {
        remainingTime += timeToAdd; // Tăng thời gian còn lại
    }
}
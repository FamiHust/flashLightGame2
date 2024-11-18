using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenu : MonoBehaviour
{
    [SerializeField] private int nextLevel;

    public void SelectLevel()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Level_" + nextLevel.ToString());
    }

    public void SelectLevel1()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}

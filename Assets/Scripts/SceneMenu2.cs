using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenu2 : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    public GameObject mapPanel;
    public GameObject levelPanel2;

    private void Awake()
    {
        ButtonToArray();
        int unlockLevel = PlayerPrefs.GetInt("unlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }


    public void OpenLevel(int levelId)
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Level_" + levelId);
    }

    public void HomeMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void SelectMenu()
    {
        SceneManager.LoadScene(2);
    }

    void ButtonToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    public void openMap2()
    {
        mapPanel.SetActive(false);
        //levelPanel1.SetActive(false);
        levelPanel2.SetActive(true);
    }


    public void selectMap()
    {
        SceneManager.LoadScene(2);
    }
}

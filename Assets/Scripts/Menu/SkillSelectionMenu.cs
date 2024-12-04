using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSkillSelectionMenu : MonoBehaviour
{
    //private Timer timer;
    public LightController lightController;
    public GameObject SelectSkillMenu;

    public Button freezeButton;
    public Button shrinkButton;
    public Button spawnButton;
    [SerializeField] private int nextLevel;

    private void Start()
    {
        //timer = FindObjectOfType<Timer>();
        lightController = FindObjectOfType<LightController>();

        freezeButton.onClick.AddListener(() => SelectLightType(LightType.Freeze));
        shrinkButton.onClick.AddListener(() => SelectLightType(LightType.Shrink));
        spawnButton.onClick.AddListener(() => SelectLightType(LightType.Spawn));
    }

    private void SelectLightType(LightType lightType)
    {
        if (lightController != null) // Kiểm tra xem lightController có null không
        {
            lightController.SelectLightType(lightType);

            // Lưu loại đèn vào PlayerPrefs
            PlayerPrefs.SetInt("SelectedLightType", (int)lightType);

            // Lưu màu sắc tương ứng với loại đèn
            Color lightColor = GetColorByLightType(lightType);
            PlayerPrefs.SetFloat("LightColorR", lightColor.r);
            PlayerPrefs.SetFloat("LightColorG", lightColor.g);
            PlayerPrefs.SetFloat("LightColorB", lightColor.b);

            PlayerPrefs.Save();
            SelectSkillMenu.SetActive(false);
        }
        else
        {
            Debug.LogError("LightController is not assigned or found!");
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_" + nextLevel.ToString());
    }

    public void SelectSkill()
    {
        Time.timeScale = 1;
        SelectSkillMenu.SetActive(true);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        SelectSkillMenu.SetActive(false);
    }

    private Color GetColorByLightType(LightType lightType)
    {
        switch (lightType)
        {
            case LightType.Freeze:
                return Color.blue;
            case LightType.Shrink:
                return Color.red;
            case LightType.Spawn:
                return Color.green;
            case LightType.Default:
            default:
                return Color.white;
        }
    }
}
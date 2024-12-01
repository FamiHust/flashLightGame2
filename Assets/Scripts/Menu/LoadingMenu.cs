using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    void Start()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsynchronously(1)); // Thay 1 bằng chỉ số cảnh bạn muốn tải
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        operation.allowSceneActivation = false;

        float simulatedProgress = 0f;

        while (!operation.isDone)
        {
            // Tính toán tiến trình thực tế
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            simulatedProgress = Mathf.MoveTowards(simulatedProgress, progress, Time.deltaTime);

            // Cập nhật thanh tiến trình
            slider.value = simulatedProgress;

            // Cập nhật văn bản phần trăm
            progressText.text = (simulatedProgress * 100f).ToString("F0") + "%";

            // Nếu quá trình tải hoàn tất
            if (simulatedProgress >= 1f)
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
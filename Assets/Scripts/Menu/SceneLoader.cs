using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public void LoadScene(int levelIndex)
    {

        loadingScreen.SetActive(true);

        StartCoroutine(LoadSceneAsynchronously(levelIndex));
    }

    IEnumerator LoadSceneAsynchronously(int levelIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        operation.allowSceneActivation = false; // Ngăn không cho chuyển cảnh ngay lập tức

        float startTime = Time.time;
        float simulatedProgress = 1f; // Biến để làm mượt thanh tiến trình

        while (!operation.isDone)
        {
            // Tính toán tiến trình thực tế
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            simulatedProgress = Mathf.MoveTowards(simulatedProgress, progress, Time.deltaTime * 1f); // Điều chỉnh tốc độ cập nhật

            // Cập nhật thanh tiến trình
            slider.value = simulatedProgress;

            // Cập nhật văn bản phần trăm
            progressText.text = (simulatedProgress * 100f).ToString("F0") + "%"; // Hiển thị số phần trăm

            // Nếu quá trình tải hoàn tất và đã đủ thời gian tối thiểu
            if (simulatedProgress >= 1f && Time.time - startTime >= 1f) // 1 giây tối thiểu
            {
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

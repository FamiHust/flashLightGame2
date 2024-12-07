using UnityEngine;
using TMPro;
using UnityEngine.UI; // Thêm thư viện này để sử dụng Button
using System.Collections;

public class TextAnim : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] Button _button; // Thêm biến cho nút
    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;
    [SerializeField] GameObject dialoguePanel;
    int i = 0;
    private bool isTextComplete = false; // Biến để theo dõi trạng thái văn bản

    public string[] stringArray;

    void Start()
    {
        EndCheck();
        _button.onClick.AddListener(OnButtonClick); // Đăng ký sự kiện nhấn nút
        _button.interactable = false; // Khóa nút khi bắt đầu
    }

    public void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        isTextComplete = false; // Đặt trạng thái văn bản là chưa hoàn thành
        _button.interactable = false; // Khóa nút khi bắt đầu hiển thị văn bản

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                isTextComplete = true; // Đặt trạng thái văn bản là đã hoàn thành
                _button.interactable = true; // Cho phép nhấn nút

                // Nếu đã đến phần tử cuối cùng, ẩn nút và văn bản
                if (i >= stringArray.Length)
                {
                    _textMeshPro.gameObject.SetActive(false);
                    _button.gameObject.SetActive(false);
                    dialoguePanel.SetActive(false);
                }
                else
                {
                    // Nếu chưa đến phần tử cuối cùng, chờ một chút trước khi tiếp tục
                    yield return new WaitForSeconds(timeBtwnWords);
                }
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }

    private void OnButtonClick()
    {
        // Nếu nhấn vào nút và đã đến phần tử cuối cùng, ẩn văn bản và nút
        if (isTextComplete && i < stringArray.Length)
        {
            isTextComplete = false; // Đặt lại trạng thái văn bản
            EndCheck(); // Tiếp tục hiển thị văn bản tiếp theo
        }
    }
}
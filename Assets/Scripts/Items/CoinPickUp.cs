using UnityEngine;
using TMPro;

public class CoinPickUp : MonoBehaviour
{
    public TextMeshProUGUI coinsText; // Tham chiếu đến TextMeshProUGUI
    public int totalCoins = 0; // Tổng số đồng xu
    private bool isPlayerInRange = false; // Kiểm tra xem người chơi có trong phạm vi không

    private void Awake()
    {
        totalCoins = PlayerPrefs.GetInt("totalCoins", 0); // Tải số lượng coin từ PlayerPrefs
        GetComponent<Collider2D>().isTrigger = true; // Đặt collider là trigger
    }

    private void Start()
    {
        UpdateCoinsText(); // Cập nhật văn bản đồng xu khi bắt đầu
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            CollectCoin(); // Thu thập coin nếu người chơi ở trong phạm vi
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true; // Người chơi vào phạm vi
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        int currentCoins = PlayerPrefs.GetInt("totalCoins", 0);
        currentCoins++; // Tăng tổng số coin
        PlayerPrefs.SetInt("totalCoins", currentCoins); // Lưu số lượng coin vào PlayerPrefs
        PlayerPrefs.Save(); // Lưu thay đổi
        UpdateCoinsText(); // Cập nhật văn bản đồng xu sau khi thu thập
        SoundManager.PlaySound(SoundType.BATTERY); // Phát âm thanh khi thu thập
        Destroy(gameObject); // Xóa đối tượng Coin
    }

    public void UpdateCoinsText()
    {
        coinsText.text = PlayerPrefs.GetInt("totalCoins", 0).ToString(); // Lấy số coin từ PlayerPrefs
    }
}
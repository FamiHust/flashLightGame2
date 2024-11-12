using UnityEngine;
using TMPro;

public class CoinPickUp : MonoBehaviour
{
    public TextMeshProUGUI coinsText; // Tham chiếu đến TextMeshProUGUI
    public static int totalCoins = 0; // Tổng số đồng xu
    private bool isPlayerInRange = false; // Kiểm tra xem người chơi có trong phạm vi không

    private void Awake()
    {
        totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Start()
    {
        UpdateCoinsText(); // Cập nhật văn bản đồng xu khi bắt đầu
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true; // Người chơi vào phạm vi
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false; // Người chơi ra khỏi phạm vi
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.P))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        totalCoins++; // Tăng tổng số coin
        PlayerPrefs.SetInt("totalCoins", totalCoins);
        UpdateCoinsText(); // Cập nhật văn bản đồng xu sau khi thu thập
        Destroy(gameObject); // Xóa đối tượng Coin
    }

    private void UpdateCoinsText()
    {
        coinsText.text = totalCoins.ToString(); // Cập nhật văn bản hiển thị số đồng xu
    }
}
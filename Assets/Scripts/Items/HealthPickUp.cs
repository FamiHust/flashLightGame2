using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthPickupAmount = 1; // Số lượng máu tăng lên
    private bool isPlayerInRange = false; // Kiểm tra xem người chơi có trong phạm vi không

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true; // Người chơi vào phạm vi
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false; // Người chơi ra khỏi phạm vi
        }
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            CollectHealth(); // Gọi phương thức thu thập máu
        }
    }

    private void CollectHealth()
    {
        Controller controller = FindObjectOfType<Controller>();
        if (controller != null)
        {
            SoundManager.PlaySound(SoundType.BATTERY);
            controller.TakeDamage(-healthPickupAmount); // Gọi hàm TakeDamage với giá trị âm để tăng máu
            Destroy(gameObject); // Xóa đối tượng Health
        }
    }
}
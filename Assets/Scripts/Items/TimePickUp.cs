using UnityEngine;

public class TimePickup : MonoBehaviour
{
    public float timeToAdd = 10f; // Số giây sẽ được thêm vào
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
            CollectTime(); // Gọi phương thức thu thập thời gian
        }
    }

    private void CollectTime()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.AddTime(timeToAdd); // Gọi phương thức AddTime trong Timer
            SoundManager.PlaySound(SoundType.BATTERY);
        }
        Destroy(gameObject); // Xóa đối tượng Time
    }
}
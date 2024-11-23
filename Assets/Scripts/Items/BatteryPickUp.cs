using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    Controller controller;
    public HealthBar batteryBar;
    public int batteryPickupAmount = 1;
    private bool isPlayerInRange = false; // Kiểm tra xem người chơi có trong phạm vi không

    void Start()
    {
        controller = FindObjectOfType<Controller>();
    }

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
            CollectBattery();
        }
    }

    private void CollectBattery()
    {
        controller.currentBattery += batteryPickupAmount;
        controller.currentBattery = Mathf.Clamp(controller.currentBattery, 0, controller.maxBattery); // Đảm bảo pin không vượt quá mức tối đa
        batteryBar.UpdateBar((int)controller.currentBattery, (int)controller.maxBattery);
        SoundManager.PlaySound(SoundType.BATTERY);
        Destroy(gameObject);
    }
}
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private LootScript lootScript; // Tham chiếu đến LootScript

    private void Start()
    {
        // Lấy tham chiếu đến LootScript trên cùng GameObject
        lootScript = GetComponent<LootScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.P))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        // Gọi phương thức calculateLoot từ LootScript
        if (lootScript != null)
        {
            lootScript.OnLootButtonClicked();
        }

        // Xóa rương
        Destroy(gameObject);
    }
}
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Controller controller;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        controller = FindObjectOfType<Controller>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Khởi tạo spriteRenderer

        spriteRenderer.enabled = false;  // Ẩn sprite
        boxCollider.enabled = true;      // Tắt collider để bẫy không thể va chạm
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.enabled = true;   
            boxCollider.enabled = true;      
            controller.TakeDamage(1);        
            SoundManager.PlaySound(SoundType.TRAP);
            SoundManager.PlaySound(SoundType.HURT);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.enabled = false;  // Ẩn sprite
            boxCollider.enabled = true;      // Tắt collider nếu cần
        }
    }
}
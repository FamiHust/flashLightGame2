using UnityEngine;

public class LightController : MonoBehaviour
{
    private Controller playerController;
    private UnityEngine.Rendering.Universal.Light2D light2D;
    private Collider2D lightCollider; // Collider cho ánh sáng

    [SerializeField] private float lightFadeSpeed = 1f; // Tốc độ giảm độ sáng
    [SerializeField] private float holdDuration = 5f; // Thời gian giữ phím để tiêu diệt kẻ thù

    private float holdTime = 0f; // Thời gian đã giữ phím

    private void Start()
    {
        playerController = FindObjectOfType<Controller>();

        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.enabled = false;

        lightCollider = GetComponent<Collider2D>();
        lightCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O) && playerController.currentBattery > 0)
        {
            if (!light2D.enabled) // Kiểm tra nếu đèn chưa bật
            {
                light2D.enabled = true;
                lightCollider.enabled = true;
                SoundManager.PlaySound(SoundType.FLASHLIGHT); // Gọi âm thanh khi bật đèn
            }

            holdTime += Time.deltaTime;

            CheckForEnemies();

            if (holdTime >= holdDuration)
            {
                DestroyEnemiesInLight();
            }
        }
        else
        {
            if (light2D.enabled)
            {
                light2D.enabled = false;
                lightCollider.enabled = false;
            }
            holdTime = 0f;
        }
    }

    private void CheckForEnemies()
    {
        Collider2D[] hitColliders = new Collider2D[10];
        int hitCount = lightCollider.Overlap(new ContactFilter2D(), hitColliders);
    }

    private void DestroyEnemiesInLight()
    {
        // Tạo một mảng để lưu các collider bị trùng
        Collider2D[] hitColliders = new Collider2D[10];
        int hitCount = lightCollider.Overlap(new ContactFilter2D(), hitColliders);

        for (int i = 0; i < hitCount; i++)
        {
            if (hitColliders[i] != null && hitColliders[i].CompareTag("Enemy"))
            {
                Destroy(hitColliders[i].gameObject);
                Debug.Log("Enemy destroyed!");
            }
        }
    }
}
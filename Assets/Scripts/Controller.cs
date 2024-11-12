using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public GameObject WinPanel;

    private SpriteRenderer spriteRenderer; // Thêm biến để lưu trữ SpriteRenderer
    private Color originalColor; // Màu sắc gốc
    public GameObject OverPanel;

    private bool isWalking = false;
    public float maxBattery = 100f; // Mức pin tối đa
    public float currentBattery; // Mức pin hiện tại
    public float batteryDrainRate = 5f; // Tốc độ tiêu thụ pin mỗi giây
    public float batteryRechargeRate = 5f; // Tốc độ sạc pin mỗi giây
    //public float batteryPickupAmount = 20f;
    private bool isCranking = false; // Kiểm tra xem có đang quay tay không

    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHealth;

    int currentHealth;

    private Rigidbody2D rb;

    public Vector2 moveInput;
    public HealthBar healthBar;
    public HealthBar batteryBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Lưu màu sắc gốc
    }

    private void Start()
    {
        if (GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }
        currentHealth = maxHealth;
        currentBattery = maxBattery;

        healthBar.UpdateBar(currentHealth, maxHealth);
        batteryBar.UpdateBar((int)currentBattery, (int)maxBattery);
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if (moveInput != Vector2.zero)
        {
            FaceMovementDirection();
        }

        // Xử lý bật tắt đèn
        if (Input.GetKey(KeyCode.O) && currentBattery > 0)
        {
            currentBattery -= batteryDrainRate * Time.deltaTime; // Giảm pin khi sử dụng đèn
            batteryBar.UpdateBar((int)currentBattery, (int)maxBattery);

            if (currentBattery == 0)
            {
                currentBattery = 0;
            }
        }

        // Xử lý nạp pin
        if (Input.GetKeyDown(KeyCode.P) && !isCranking)
        {
            isCranking = true;
            StartCoroutine(CrankBattery());
            SoundManager.PlaySound(SoundType.BATTERY);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            isCranking = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void FaceMovementDirection()
    {
        float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg; // Chuyển đổi radian sang độ

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Tạo góc quay
        transform.rotation = rotation;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
        {
            currentHealth = 0;
            GameOver();
            SoundManager.PlaySound(SoundType.PLAYERDIE);
        }
        healthBar.UpdateBar(currentHealth, maxHealth);

        StartCoroutine(FlashGrey());
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        OverPanel.SetActive(true);
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            SoundManager.PlaySound(SoundType.ATTACK);
            StartCoroutine(PlayHurtSoundWithDelay(0.4f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            GameWin();
        }
    }

    private IEnumerator PlayHurtSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SoundManager.PlaySound(SoundType.HURT);
    }

    private IEnumerator CrankBattery()
    {
        while (isCranking)
        {
            currentBattery += batteryRechargeRate * Time.deltaTime; // Sạc pin khi nhấn giữ phím
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery); // Đảm bảo pin không vượt quá mức tối đa
            batteryBar.UpdateBar((int)currentBattery, (int)maxBattery);

            yield return null; // Chờ cho đến khung hình
        }
    }

    private IEnumerator FlashGrey()
    {
        spriteRenderer.color = Color.grey; // Đổi màu sắc sang đỏ
        yield return new WaitForSeconds(0.08f); // Thời gian nhấp nháy
        spriteRenderer.color = originalColor; // Khôi phục lại màu sắc gốc
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject WinPanel;

    private SpriteRenderer spriteRenderer; // Thêm biến để lưu trữ SpriteRenderer
    private Animator animator;
    private Color originalColor; // Màu sắc gốc
    public GameObject OverPanel;
    public Joystick joystick;
    private bool isDie = false;
    private bool isGameOver = false;

    private bool isWalking = false;
    public float maxBattery = 100f; // Mức pin tối đa
    public float currentBattery; // Mức pin hiện tại
    public float batteryDrainRate = 5f; // Tốc độ tiêu thụ pin mỗi giây
    float batteryRechargeRate = 0f; // Tốc độ sạc pin mỗi giây
    private bool isCranking = false; // Kiểm tra xem có đang quay tay không
    private bool isOpenFlash = false;

    [SerializeField] private float moveSpeed;
    public int maxHealth;
    [SerializeField] private float rotationSpeed = 5f; // Tốc độ quay
    [SerializeField] private Button btnOpenFlash;

    public int currentHealth;

    private Rigidbody2D rb;

    public Vector2 moveInput;
    public HealthBar healthBar;
    public HealthBar batteryBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Lưu màu sắc gốc
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

        if (GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }

        // Khôi phục giá trị maxHealth và maxBattery từ PlayerPrefs
        maxHealth = PlayerPrefs.GetInt("maxHealth", maxHealth);
        maxBattery = PlayerPrefs.GetInt("maxBattery", (int)maxBattery);
        moveSpeed = PlayerPrefs.GetFloat("moveSpeed", 3.5f);
        currentHealth = maxHealth;
        currentBattery = maxBattery;

        healthBar.UpdateBar(currentHealth, maxHealth);
        batteryBar.UpdateBar((int)currentBattery, (int)maxBattery);
    }

    private void Update()
    {
        if (isGameOver) // Kiểm tra nếu game over
        {
            return; // Không thực hiện gì thêm
        }

        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // moveInput.x = joystick.Horizontal;
        // moveInput.y = joystick.Vertical;

        if (moveInput != Vector2.zero)
        {
            FaceMovementDirection();
            animator.SetBool("isWalking", true); // Bật animation walk
        }
        else
        {
            animator.SetBool("isWalking", false); // Tắt animation walk
        }

        if (isOpenFlash && currentBattery > 0)
        {
            currentBattery -= batteryDrainRate * Time.deltaTime; // Giảm pin khi sử dụng đèn
            batteryBar.UpdateBar((int)currentBattery, (int)maxBattery);

            if (currentBattery == 0)
            {
                currentBattery = 0;
            }
        }
        else if (!isOpenFlash) // Nếu đèn không mở, sạc pin
        {
            currentBattery += batteryRechargeRate * Time.deltaTime; // Sạc pin
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery); // Đảm bảo pin không vượt quá mức tối đa
            batteryBar.UpdateBar((int)currentBattery, (int)maxBattery);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void TurnFlash()
    {
        if (isOpenFlash)
        {
            isOpenFlash = false;
        }
        else
        {
            isOpenFlash = true;
        }

    }

    private void FaceMovementDirection()
    {
        if (moveInput != Vector2.zero) // Ensure there's movement input
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg; // Convert radians to degrees
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Create target rotation

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed); // Use Lerp for smooth rotation
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
        {
            currentHealth = 0;
            GameOver();
            //SoundManager.PlaySound(SoundType.PLAYERDIE);
        }
        healthBar.UpdateBar(currentHealth, maxHealth);

        StartCoroutine(FlashGrey());
    }

    public void GameOver()
    {
        isGameOver = true;
        animator.SetBool("isDie", true);
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 0;
        OverPanel.SetActive(true);
        SoundManager.PlaySound(SoundType.PLAYERDIE);
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
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy != null && !enemy.IsFrozen())
            {
                TakeDamage(1);
                SoundManager.PlaySound(SoundType.ATTACK);
                StartCoroutine(PlayHurtSoundWithDelay(0.4f));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            UnlockNewLevel();
            GameWin();
            SoundManager.PlaySound(SoundType.VICTORY);
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("unlockedLevel", PlayerPrefs.GetInt("unlockedLevel", 1) + 1);
            PlayerPrefs.Save();
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

    // private IEnumerator PlayDieSoundWithDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     SoundManager.PlaySound(SoundType.PLAYERDIE);
    // }
}
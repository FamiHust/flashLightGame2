using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int playerLives = 3;

    private Rigidbody2D rb;

    public Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if (moveInput != Vector2.zero)
        {
            FaceMovementDirection();
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
        transform.rotation = rotation; // Áp dụng góc quay cho nhân vật
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerLives--;
            Debug.Log("Player hit! Lives remaining: " + playerLives);

            if (playerLives <= 0)
            {
                Debug.Log("Player has died!");
                Destroy(gameObject);
                Time.timeScale = 0;
            }
        }
    }
}
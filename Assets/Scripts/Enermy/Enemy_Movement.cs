using UnityEngine;
using Pathfinding;


public class Enemy_Movement : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    public GameObject player;
    public GameObject spotlight;
    private AIPath aiPath;
    private Animator animator;
    private float originalSpeed;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        originalSpeed = aiPath.maxSpeed; // Lưu tốc độ gốc
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the enemy enters the spotlight's range
        if (collision.gameObject == spotlight)
        {
            SoundManager.PlaySound(SoundType.DIE);
        }
    }

    public void StopMovement()
    {
        aiPath.maxSpeed = 0f; // Đặt tốc độ về 0
        animator.enabled = false;
    }

    public void ResumeMovement()
    {
        aiPath.maxSpeed = 2f; // Đặt lại tốc độ ban đầu (hoặc giá trị bạn muốn)
        animator.enabled = true; // Bật lại animator nếu cần
    }

    public void SetSpeed(float newSpeed)
    {
        aiPath.maxSpeed = newSpeed; // Cập nhật tốc độ
    }

    public void ResetSpeed()
    {
        aiPath.maxSpeed = originalSpeed; // Khôi phục tốc độ gốc
    }
}

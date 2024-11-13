using UnityEngine;
using Pathfinding;


public class Enemy_Movement : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    public GameObject player;
    public GameObject spotlight;
    private AIPath aiPath;
    private Animator animator;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
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

    public void ReduceSpeed(float factor)
    {
        aiPath.maxSpeed *= factor; // Giảm tốc độ
    }
}

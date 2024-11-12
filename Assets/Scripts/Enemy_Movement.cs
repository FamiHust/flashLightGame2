using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private EnemySpawner enemySpawner;
    public GameObject player;
    public GameObject spotlight;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the enemy enters the spotlight's range
        if (collision.gameObject == spotlight)
        {
            // Destroy the enemy
            SoundManager.PlaySound(SoundType.DIE);

            Destroy(gameObject, 0.1f);
        }
    }
}

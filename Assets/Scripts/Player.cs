using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object the enemy collided with has the tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerLives--;
            Debug.Log("Player hit! Lives remaining: " + playerLives);

            if (playerLives <= 0)
            {
                Debug.Log("Player has died!");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Destroy(gameObject);
                Time.timeScale = 0;

            }
        }
    }
}

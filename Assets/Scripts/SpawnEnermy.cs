using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của kẻ thù
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn
    public Vector2 spawnAreaMin; // Điểm bắt đầu của khu vực spawn
    public Vector2 spawnAreaMax; // Điểm kết thúc của khu vực spawn

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is missing! Continuing to spawn without creating enemies.");
            return; // Dừng lại nếu prefab không tồn tại
        }
        // Tính toán vị trí spawn ngẫu nhiên trong khu vực đã chỉ định
        float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        // Spawn kẻ thù tại vị trí ngẫu nhiên
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
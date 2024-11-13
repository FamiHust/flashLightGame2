using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public float spawnRate = 2f; // Spawn every 2 seconds
    private float timeUntilSpawn;

    private Spawn_Area[] spawners;

    void Start()
    {
        spawners = FindObjectsByType<Spawn_Area>(FindObjectsSortMode.None);
        timeUntilSpawn = spawnRate;
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            SpawnEnemy();
            timeUntilSpawn = spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        if (spawners.Length == 0) return;

        // Select a random spawner
        int randomIndex = Random.Range(0, spawners.Length);
        Spawn_Area selectedSpawner = spawners[randomIndex];

        // Get a random spawn position from the selected spawner
        Vector2 spawnPosition = selectedSpawner.GetRandomSpawnPosition();
        Instantiate(Enemy, spawnPosition, Quaternion.identity);
    }
}

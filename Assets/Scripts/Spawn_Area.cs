using UnityEngine;

public class Spawn_Area : MonoBehaviour
{
    public BoxCollider2D spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        // if (spawnArea == null)
        // {
        //     Debug.LogError("BoxCollider2D component is missing.");
        // }
    }

    public Vector2 GetRandomSpawnPosition()
    {
        if (spawnArea == null) return Vector2.zero;

        Bounds bounds = spawnArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }

    // Draw the spawn area in the editor for visualization
    private void OnDrawGizmosSelected()
    {
        if (spawnArea == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnArea.bounds.center, spawnArea.bounds.size);
    }
}

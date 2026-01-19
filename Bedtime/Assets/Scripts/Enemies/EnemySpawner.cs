using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;

    [Header("Global Enemy Limit")]
    public int maxEnemies = 10;

    public static int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        currentEnemyCount++;

        // Assign target
        NavTest nav = enemy.GetComponent<NavTest>();
        if (nav != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                nav.target = player.transform;
        }
    }
}

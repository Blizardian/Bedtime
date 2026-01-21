using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public GameObject enemyPrefab;
    public float spawnInterval = 3f;

    [Header("Global Enemy Limit")]
    public static int maxEnemies = 10;

    public static int currentEnemyCount = 0;

    void Start()
    {
            InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);

        currentEnemyCount = 0;
    }

    private void Update()
    {
        EnemyCountStabilizer();

        Debug.Log("Current" + currentEnemyCount);
        Debug.Log("Max" + maxEnemies);
    }

    public void ResetCurrentEnemyCount()
    {
        currentEnemyCount = 0;
    }

    private void EnemyCountStabilizer()
    {
        if (currentEnemyCount < 0)
        {
            currentEnemyCount = 0;
        }
    }

    void SpawnEnemy()
    {
        if (PlayerStats.Instance.levelTracker == 1)
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
}

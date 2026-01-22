using UnityEngine;

public class EnemySpawnerMelee : MonoBehaviour
{
    public static EnemySpawnerMelee Instance;

    public GameObject enemyPrefab;
    public float spawnInterval = 3f;

    [Header("Global Enemy Limit")]
    public static int maxEnemies = 2;

    public static int currentEnemyCount = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);

        ResetCurrentEnemyCount(); ; // Sets the enemycount to 0
    }

    private void Update()
    {
        EnemyCountStabilizer();

        Debug.Log("Current" + currentEnemyCount);
        Debug.Log("Max" + maxEnemies);
    }

    //Resets the current ammount of enemies
    public void ResetCurrentEnemyCount()
    {
        currentEnemyCount = 0;
    }

    /// <summary>
    /// Stabilizes the enemy count to be 0 if the value is less then 0, so the spawning works as it should be
    /// </summary>
    private void EnemyCountStabilizer()
    {
        if (currentEnemyCount < 0)
        {
            currentEnemyCount = 0;
        }
    }

    /// <summary>
    /// Spawn a enemy
    /// </summary>
    void SpawnEnemy()
    {
        if (PlayerStats.Instance.levelTracker == 1)
        {
            if (currentEnemyCount >= maxEnemies)
                return;

            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            currentEnemyCount++;

            // Assign target
            NavMeshEnemy nav = enemy.GetComponent<NavMeshEnemy>();
            if (nav != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                    nav.target = player.transform;
            }
        }
    }
}

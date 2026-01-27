using UnityEngine;
using UnityEngine.UI;

public class Enemy1Behaviour : MonoBehaviour
{
    public int enemy1Health;
    public Transform target;
    [SerializeField] private float explodeRange;

    // Timer
    [SerializeField] float explodeTimer;
    [SerializeField] float explodeTimerMax;
    [SerializeField] bool explodeTimerIsOn;

    // Show Range
    [SerializeField] GameObject explodeRangePrefab;
    GameObject showExplodeRange;
    [SerializeField] float showRange;
    [SerializeField] float preventExplosionRange;

    //HealthBar
    [Tooltip("This needs to be assigned manually!")]
    public Slider healthBar; // Set in the inspector
    [Tooltip("This needs to be assigned manually!")]
    public GameObject HealthBarObject; // Set in the inspector

    // Other scripts
    public GameObject SpawnerPart;
    public static EnemySpawner spawnerScript;

    // Partical
    [Tooltip("This needs to be assigned manually!")]
    public GameObject explosionEffect;
    void Start()
    {
        SpawnerPart = GameObject.Find("Ball pit L");
        spawnerScript = SpawnerPart.GetComponent<EnemySpawner>();

        SetIfZero();

        showExplodeRange = Instantiate(explodeRangePrefab, transform.position, Quaternion.identity); // Instantiate a unique circle for this enemy
        showExplodeRange.transform.SetParent(transform); // So it sets it as a parent and moves with the enemy
    }

    /// <summary>
    /// Sets some value's or inspector related stuff if they aren't set (So the game won't break)
    /// </summary>
    private void SetIfZero()
    {
        if (explodeTimerMax == 0) // Sets the explodeTimerMax if it hasn't been set
        {
            explodeTimerMax = 1; // Sets the value
        }
        if (showRange == 0) // Sets the showRange if it hasn't been set
        {
            showRange = 8; // Sets the value
        }
        if (explodeRange == 0) // Sets the explodeRange if it hasn't been set
        {
            explodeRange = 3; // Sets the value
        }

        if (target == null) // If the target is not set
        {
            target = GameObject.FindWithTag("Player").transform; // Sets the target tot the player
        }

        if (enemy1Health == 0) // Sets the health if it has not been set
        {
            enemy1Health = 100; // Healt is set to a value
        }

        if(preventExplosionRange == 0)
        {
            preventExplosionRange = 8; // Sets the range for when the explosion gets canceled.
        }
    }

    void Update()
    {

        // DestroyAllEnemies();

        UpdateEnemyHealth();

        if (enemy1Health <= 0) // When the health is less or equal to 0
        {
            SpawnerIntUpdater();
            PlayerStats.Instance.playerScore += PlayerStats.Instance.scoreReceivedOnKill; // Adds a score when this enemy dies
            Destroy(gameObject);

        }

        if (target) // If there is a target
        {
            float distance = Vector3.Distance(target.position, transform.position); // Calculate the distance between the player and the agent

            if (distance < explodeRange)
            {
                if (!explodeTimerIsOn)
                {
                    explodeTimerIsOn = true;

                    Debug.Log("Explosion incoming");
                }
            }

            ActivateExplodeTimer();

            if (explodeTimer >= explodeTimerMax)
            {
                if (distance > preventExplosionRange)
                {
                    Debug.Log("Stopped the explosion");
                    ResetExplodeTimer();
                }
                else
                {
                    if (distance <= explodeRange)
                    {
                        Debug.Log("Player will get damage");
                        PlayerStats.Instance.HP -= 50;
                        UIManager.Instance.ShowHitIndicator(); // Notify the player that he received damage
                    }
                    else
                    {
                        Debug.Log("Player will not get damage");
                    }

                    SpawnerIntUpdater();
                    Instantiate(explosionEffect, transform.position,transform.rotation);
                    Destroy(gameObject);

                }
            }

            if (distance < showRange)
            {
                ShowExplodeRangeInGame();
                HealthBarObject.SetActive(true);
            }
            else
            {
                showExplodeRange.SetActive(false);
                HealthBarObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Destroys all enemies when level is completed
    /// </summary>
    public void DestroyAllEnemies()
    {
        if(PlayerStats.Instance.playerScore == PlayerStats.Instance.scoreNeededLevel1) // Needs to be upgraded so that when you are in a new level it still spawns, since now it does not
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy1");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }

    /// <summary>
    /// Updates the spawner when a enemy gets killed
    /// </summary>
    private  void SpawnerIntUpdater()
    {
        EnemySpawner.currentEnemyCount--; // Updates the current enemy count with one less
    }

    private void UpdateEnemyHealth()
    {
        healthBar.value = enemy1Health; // Sets the slider to the to ammount of the health (This updates the HP bar)
    }

    /// <summary>
    /// Resets the timer of the explosion
    /// </summary>
    private void ResetExplodeTimer()
    {
        explodeTimerIsOn = false;
        explodeTimer = 0;
    }

    /// <summary>
    /// Checks if the Explosion timer should be counting or not (This is set in update)
    /// </summary>
    private void ActivateExplodeTimer()
    {
        if (explodeTimerIsOn == true)
        {
            explodeTimer += Time.deltaTime;
        }
        else
        {
            explodeTimer = 0;
        }
    }
    // Draws gizmo in the scene, so you can see the range of the explosion and the range in when the explosion is deactivated
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Changes the color
        Gizmos.DrawWireSphere(transform.position, explodeRange); // Draws a sphere of on the position and range of explodeRange for the object the script is attached to.
        Gizmos.DrawWireSphere(transform.position, preventExplosionRange);
    }

    /// <summary>
    /// Shows the circle ingame and scales it correctly
    /// </summary>
    void ShowExplodeRangeInGame()
    {
        Vector3 correctScale = new Vector3(explodeRange * 2, 0.0001f, explodeRange * 2);
        Vector3 position = new Vector3(0, -1.08f, 0);
        showExplodeRange.transform.localPosition = position;
        showExplodeRange.transform.localScale = correctScale;
        showExplodeRange.SetActive(true);
    }
}

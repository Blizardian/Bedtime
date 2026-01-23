using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MeleeEnemy : MonoBehaviour
{
    public static MeleeEnemy instance;

    public NavMeshAgent agent;
    public Transform player;

    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;

    // Healthbar
    [Tooltip("This needs to be assigned manually!")]
    public Slider healthBar;
    [Tooltip("This needs to be assigned manually!")]
    public GameObject healthBarObject;
    public int HP;
    public int maxHP;
    [SerializeField] float showRange;
    public void Start()
    {
        //healthBarObject = GameObject.Find("HealthBarMelee"); // Automaticly assign the healthBarObject Gameobject
        player = GameObject.Find("Player").transform; // Automaticly assign player

        maxHP = 200; // Set the maxHP

        HP = maxHP; // Set the player's HP

        if(showRange == 0) // If the range has not been set, set it automaticly
        {
            showRange = 8; // Set the value
        }
    }
    void Update()
    {
        float agentRadius = agent.radius; // Acces to the agent's radius
        float distance = Vector3.Distance(player.position, agent.transform.position); // Calculate the distance between the player and the agent


        if (distance <= agentRadius && Time.time >= nextAttackTime)
        {
            DamagePlayer();
            nextAttackTime = Time.time + attackCooldown;
        }

        healthBar.value = HP; // Updates the value of the healthbar with the value of the HP
        DestroyOnZeroHP();

        if (distance < showRange) // When the distance is less then the showrange value, it does the following
        {
            healthBarObject.SetActive(true); // Show the HP bar
        }
        else
        {
            healthBarObject.SetActive(false); // Don't show the HP bar
        }
    }

    /// <summary>
    /// Updates the spawner when a enemy gets killed
    /// </summary>
    private void SpawnerIntUpdater()
    {
        EnemySpawnerMelee.currentEnemyCount--; // Updates the current enemy count with one less
    }

    /// <summary>
    /// Damage the player
    /// </summary>
    void DamagePlayer()
    {
        PlayerStats.Instance.HP -= 25;
        Debug.Log("Player hit!");
    }

    /// <summary>
    /// Destroy when HP hits 0 or less then 0
    /// </summary>
    void DestroyOnZeroHP()
    {
        if(HP <= 0)
        {
            SpawnerIntUpdater();
            PlayerStats.Instance.playerScore += PlayerStats.Instance.scoreReceivedOnKill; // Adds a score when this enemy dies
            Destroy(gameObject);
            
        }
    }
}
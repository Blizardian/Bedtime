using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class MeleeEnemy : MonoBehaviour
{
    public NavMeshAgent agent;   // Drag your NavMeshAgent here
    public Transform player;

    public float attackCooldown = 1.5f; // seconds between hits
    private float nextAttackTime = 0f;

    // Healthbar
    [Tooltip("This needs to be assigned manually!")]
    public Slider healthBar;
    public GameObject healthBarObject;
    public static int HP;
    public int maxHP;
    [SerializeField] float showRange;
    public void Start()
    {
        healthBarObject = GameObject.Find("HealthBarMelee");
        player = GameObject.Find("Player").transform;

        maxHP = 100;

        HP = maxHP;

        if(showRange == 0)
        {
            showRange = 8;
        }
    }
    void Update()
    {
        float agentRadius = agent.radius; // <-- Access the NavMeshAgent's radius
        float distance = Vector3.Distance(player.position, agent.transform.position);


        if (distance <= agentRadius && Time.time >= nextAttackTime)
        {
            DamagePlayer();
            nextAttackTime = Time.time + attackCooldown;
        }

        healthBar.value = HP; // Updates the value of the healthbar with the value of the HP
        DestroyOnZeroHP();

        if (distance < showRange)
        {
            healthBarObject.SetActive(true);
        }
        else
        {
            healthBarObject.SetActive(false);
        }
    }

    void DamagePlayer()
    {
        PlayerStats.Instance.HP -= 25;
        Debug.Log("Player hit!");
    }

    void DestroyOnZeroHP()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
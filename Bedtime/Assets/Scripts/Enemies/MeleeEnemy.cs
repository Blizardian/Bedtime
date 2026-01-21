using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public NavMeshAgent agent;   // Drag your NavMeshAgent here
    public Transform player;

    public float attackCooldown = 1.5f; // seconds between hits
    private float nextAttackTime = 0f;
    void Update()
    {
        float agentRadius = agent.radius; // <-- Access the NavMeshAgent's radius
        float distance = Vector3.Distance(player.position, agent.transform.position);


        if (distance <= agentRadius && Time.time >= nextAttackTime)
        {
            DamagePlayer();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void DamagePlayer()
    {
        PlayerStats.Instance.HP -= 25;
        Debug.Log("Player hit!");
    }
}
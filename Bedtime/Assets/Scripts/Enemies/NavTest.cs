using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}
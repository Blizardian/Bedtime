using UnityEngine;
using UnityEngine.AI;
public class NavMeshEnemy : MonoBehaviour
{
    public Transform target;
    Vector3 destination;
    NavMeshAgent agent;
    int distanceDifference;

    void Start()
    {
        distanceDifference = 1;
        target = GameObject.FindWithTag("Player").transform; //  Sets the target of the enemy
        agent = GetComponent<NavMeshAgent>(); // Tells the script that agent is the NavMeshAgent component of the object this script is attached to
        destination = agent.destination;
    }

    void Update()
    {
        if (Vector3.Distance(destination, target.position) > distanceDifference) // Updates when the difference is bigger then the value of distanceDifference
        {
            destination = target.position; // Updates the vector destination of the agent to the position of the target
            agent.destination = destination; // Sets the agents's destination to the value of destination
        }
    }
}

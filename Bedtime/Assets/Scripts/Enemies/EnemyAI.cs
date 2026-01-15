using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;                 // The player we want to chase

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;             // How fast the enemy moves
    [SerializeField] private float chaseRange = 20f;           // How close the player must be before enemy starts chasing
    [SerializeField] private float stoppingDistance = 1.5f;    // Minimum distance enemy stops so it doesn't clip into the player

    [SerializeField] private float positionLerpSpeed = 8f;     // How smooth the enemy moves (higher = faster smoothing)
    [SerializeField] private float rotationLerpSpeed = 10f;    // How smooth the enemy rotates (higher = faster turning)

    [Header("Random Wandering")]
    [SerializeField] private float randomMoveRadius = 10f;     // How far random wander targets can be
    [SerializeField] private float randomMoveInterval = 3f;    // How often a new random point is chosen

    private Vector3 randomTarget;                              // Current wander destination
    private float randomTimer;                                 // Timer for choosing new wander point

    [Header("Damage")]
    [SerializeField] private int touchDamage = 10;             // How much damage the enemy deals to the player
    [SerializeField] private float touchDamageCooldown = 1f;   // Delay between hits while touching the player
    [SerializeField] private float knockbackForce = 6f;        // How hard the enemy pushes the player back

    private float nextDamageTime = 0f;                         // Internal timer to control hit cooldown

    private void Start()
    {
        // Auto-find the player by tag if not manually assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        PickRandomTarget(); // Pick first wander target
    }

    private void Update()
    {
        if (player == null) return; // If no player found, stop

        // Calculate distance and direction to the player
        Vector3 toPlayer = player.position - transform.position; // Vector toward player
        toPlayer.y = 0f;                                         // Keep enemy level vertically
        float distance = toPlayer.magnitude;                     // How far away the player is

        if (distance <= chaseRange)
        {
            // Player close enough → chase them
            ChasePlayer(toPlayer, distance);
        }
        else
        {
            // Player far → wander randomly
            Wander();
        }
    }

    private void ChasePlayer(Vector3 toPlayer, float distance)
    {
        // Only move if not too close to the player
        if (distance > stoppingDistance)
        {
            Vector3 direction = toPlayer.normalized;                                // Direction toward player
            Vector3 targetPos = transform.position + direction * moveSpeed * Time.deltaTime; // Where enemy SHOULD go this frame

            float posT = positionLerpSpeed * Time.deltaTime;                         // Smoothing factor
            transform.position = Vector3.Lerp(transform.position, targetPos, posT);  // Smooth movement
        }

        // Smooth rotation to face the player
        Quaternion targetRot = Quaternion.LookRotation(toPlayer, Vector3.up);        // Desired rotation
        float rotT = rotationLerpSpeed * Time.deltaTime;                             // Smoothing factor
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotT);  // Smooth turning
    }

    private void Wander()
    {
        // Count down until we choose a new random point
        randomTimer -= Time.deltaTime;
        if (randomTimer <= 0f)
            PickRandomTarget();

        // Move toward the random target
        Vector3 toTarget = randomTarget - transform.position; // Direction to wander point
        toTarget.y = 0f;

        // If very close, pick a new target immediately
        if (toTarget.sqrMagnitude < 0.1f)
        {
            PickRandomTarget();
            return;
        }

        Vector3 direction = toTarget.normalized;                                // Direction toward wander point
        Vector3 targetPos = transform.position + direction * moveSpeed * Time.deltaTime;

        float posT = positionLerpSpeed * Time.deltaTime;                         // Smoothing factor
        transform.position = Vector3.Lerp(transform.position, targetPos, posT);  // Smooth movement

        Quaternion targetRot = Quaternion.LookRotation(toTarget, Vector3.up);    // Face wander direction
        float rotT = rotationLerpSpeed * Time.deltaTime;                         // Smoothing factor
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotT);
    }

    private void PickRandomTarget()
    {
        // Pick a random offset around the enemy
        Vector3 offset = new Vector3(
            Random.Range(-randomMoveRadius, randomMoveRadius),
            0f,
            Random.Range(-randomMoveRadius, randomMoveRadius)
        );

        randomTarget = transform.position + offset; // Final wander point
        randomTimer = randomMoveInterval;          // Reset wander timer
    }

    /* private void OnTriggerEnter(Collider other)
     {
         // Damage player when first touching
         if (other.CompareTag("Player"))
             TryDamageAndKnockback(other);
     }

     private void OnTriggerStay(Collider other)
     {
         // Damage player while staying inside trigger (cooldown controlled)
         if (other.CompareTag("Player"))
             TryDamageAndKnockback(other);
     }

     private void TryDamageAndKnockback(Collider playerCol)
     {
         // ❌ If cooldown not done → do NOTHING
         if (Time.time < nextDamageTime) return;

         // ✔ Damage happens here
         PlayerHealth ph = playerCol.GetComponent<PlayerHealth>();
         if (ph != null)
         {
             ph.TakeDamage(touchDamage);
             nextDamageTime = Time.time + touchDamageCooldown;

             // ✔ Knockback only when damage is applied
             Rigidbody rb = playerCol.attachedRigidbody;
             if (rb != null)
             {
                 Vector3 dir = (rb.position - transform.position);
                 dir.y = 0f;

                 if (dir.sqrMagnitude > 0.01f)
                     dir.Normalize();
                 else
                     dir = transform.forward;

                 rb.AddForce(dir * knockbackForce, ForceMode.VelocityChange);
             }
         }
     }*/
}
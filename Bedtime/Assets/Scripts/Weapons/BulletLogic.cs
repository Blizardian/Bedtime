using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Bullet Settings
    [SerializeField] private float bulletSpeed;

    // Rigidbody
    private Rigidbody rb;
    public static EnemySpawner spawnerScriptR;
    void Start()
    {
        bulletSpeed = 60f; // Sets the bullet speed
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletSpeed; // Immediately move forward
        Destroy(gameObject, 2f); //  Destroys the bullets after 2 seconds (If nothing is hit)
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy1"))
        {
            Enemy1Behaviour enemy = collision.gameObject.GetComponent<Enemy1Behaviour>();

            if (enemy != null)
            {
                enemy.enemy1Health -= 50;
                Debug.Log("Damage done");
            }
            Destroy(gameObject); //  Destroy the bullet
            return;
        }

        Destroy(gameObject); Destroy(gameObject); //  Destroy the bullet
        Debug.Log("Hitting anything");
    }
}

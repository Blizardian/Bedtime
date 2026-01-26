using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Bullet Settings
    [SerializeField] private float bulletSpeed;

    // Rigidbody
    private Rigidbody rb;
    public static EnemySpawner spawnerScriptR;


    // Effect
    public GameObject bloodEffect;
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
                Instantiate(bloodEffect,collision.gameObject.transform.position, collision.gameObject.transform.rotation); // blood effect
                Debug.Log("Damage done");
            }
            Destroy(gameObject); //  Destroy the bullet
            return;
        }

        if (collision.gameObject.CompareTag("MeleeEnemy"))
        {
            MeleeEnemy enemy = collision.gameObject.GetComponent<MeleeEnemy>();

            if (enemy != null)
            {
                enemy.HP -= 25;
                Instantiate(bloodEffect, collision.gameObject.transform.position, collision.gameObject.transform.rotation); // blood effect
                Debug.Log("Melee enemy hit");
            }

            Destroy(gameObject);
            return;
        }

        Destroy(gameObject); Destroy(gameObject); //  Destroy the bullet
        Debug.Log("Hitting anything");
    }
}

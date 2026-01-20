using UnityEngine;

public class TurretBulletBehaviour : MonoBehaviour
{
    public GameObject target;
    public float bulletSpeed;

    private Rigidbody rb;

    void Start()
    {
        if(bulletSpeed == 0)
        {
            bulletSpeed = 60;
        }

        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player");

        Vector3 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;

        Destroy(gameObject, 5f); // After 5 seconds destroy if nothing was hit
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerStats.Instance.HP -= 20;
            }

            Debug.Log("Bullet Destroyed");
            Destroy(gameObject);
        }
    }
}

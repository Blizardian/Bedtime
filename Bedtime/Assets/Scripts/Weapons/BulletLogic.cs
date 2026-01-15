using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // Bullet Settings
    [SerializeField] private float bulletSpeed = 20f;

    // Rigidbody
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * bulletSpeed; // Immediately move forward
        Destroy(gameObject, 5f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

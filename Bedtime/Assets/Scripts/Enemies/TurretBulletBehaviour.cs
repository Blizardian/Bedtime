using UnityEngine;

public class TurretBulletBehaviour : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private float bulletSpeed;

    private Rigidbody rb;

    void Start()
    {
        if(bulletSpeed == 0)
        {
            bulletSpeed = 60;
        }

        rb = GetComponent<Rigidbody>();
        target = GameObject.Find("Player"); // target is set to the Player

        Vector3 direction = (target.transform.position - transform.position).normalized; // Determine the direction
        rb.linearVelocity = direction * bulletSpeed; // The bullet will go to the direction with the speed of the bulletSpeed variable

        Destroy(gameObject, 5f); // After 5 seconds destroy if nothing was hit
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerStats.Instance.HP -= 20; // Player receives damage
                UIManager.Instance.ShowHitIndicator(); // Notify the player that he received damage
            }
            Debug.Log("Bullet Destroyed");
            Destroy(gameObject);
        }
    }
}

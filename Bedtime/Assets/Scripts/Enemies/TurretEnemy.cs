using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;

    public GameObject target;

    [SerializeField] private float fireRate;
    private float timer;

    private void Awake()
    {
        fireRate = Random.Range(2, 10); // Fire rate will be random per turret (Between the 2 values)
        target = GameObject.Find("Player");
    }
    void Update()
    {
        if (PlayerStats.Instance.levelTracker == 1 && gameObject.CompareTag("TurretLevel1"))
        {
            transform.LookAt(target.transform); // Always look at the player

            timer += Time.deltaTime;

            if (timer >= fireRate)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

                Debug.Log("Turret Fired Bullet!");
                timer = 0f;
            }
        }
    }
}

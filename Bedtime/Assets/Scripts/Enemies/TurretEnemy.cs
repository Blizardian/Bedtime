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
        fireRate = Random.Range(3, 8);
        target = GameObject.Find("Player");
    }
    void Update()
    {
        if (PlayerStats.Instance.levelTracker == 1)
        {
            transform.LookAt(target.transform);

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

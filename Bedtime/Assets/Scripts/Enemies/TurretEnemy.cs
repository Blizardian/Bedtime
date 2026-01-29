using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawn;

    public GameObject target;

    [SerializeField] private float fireRate;
    private float timer;
    private float resetTimer = 0;

    private void Awake()
    {
        fireRate = Random.Range(2, 10); // Fire rate will be random per turret (Between the 2 values)
        target = GameObject.Find("Player");
    }
    void Update()
    {
        if (PlayerStats.Instance.StageTracker == 5 && gameObject.CompareTag("TurretLevel1"))
        {
            transform.LookAt(target.transform); // Always look at the player

            timer += Time.deltaTime; // timer's value will increment with time

            if (timer >= fireRate)
            {
                Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation); // Spawn the bullet at the right position and rotation

                Debug.Log("Turret Fired Bullet!");
                timer = resetTimer; // Resets the timer
            }
        }
    }
}

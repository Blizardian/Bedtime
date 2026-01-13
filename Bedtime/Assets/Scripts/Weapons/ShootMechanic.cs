using UnityEngine;

public class ShootMechanic : MonoBehaviour
{
    public GameObject Camera;

    public GameObject bullet;
    public Transform spawnPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera");
            Debug.Log("INFO: Player has been automatically set");
        }

        spawnPosition = Camera.transform.Find("BulletSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject newBullet = Instantiate(bullet, spawnPosition.position, spawnPosition.rotation);
        }
    }
}

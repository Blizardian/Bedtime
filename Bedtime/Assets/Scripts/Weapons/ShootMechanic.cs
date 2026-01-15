using UnityEngine;
using UnityEngine.UI;
public class ShootMechanic : MonoBehaviour
{
    public GameObject Camera;

    public GameObject bullet;
    public Transform spawnPosition;

    public float timer;
    private float timerLimit;
    public bool timerIsOn;

    public Slider ReloadCooldownSlider;

    [SerializeField] private int ammoInWeapon;
    //[Range(0, 1000)][SerializeField] private int totalAmmoAmmount;
    [SerializeField] private int maxAmmo;

    void Start()
    {
        timerLimit = 3;

        spawnPosition = Camera.transform.Find("BulletSpawn");
        //totalAmmoAmmount = 45;
        maxAmmo = 15;
        ammoInWeapon = maxAmmo;

        AssignForgottenAtStart();
        ReloadCooldownTimer();

    }
    private void AssignForgottenAtStart()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera");
            Debug.Log("INFO: Player has been automatically set");
        }
    }

    void Update()
    {
        if (timerIsOn == true)
        {
            Reload();
        }

        Shooting();

        RunTimerReload();
    }

    /// <summary>
    /// Enables the timer so the player can reload
    /// </summary>
    private void RunTimerReload()
    {
        //Reload Mechanic if we have infinite ammo
        if (Input.GetKeyDown(KeyCode.R) && timerIsOn == false)
        {
            timerIsOn = true;
        }

        //Reload Mechanic if we have limited ammo
        /*if (Input.GetKeyDown(KeyCode.R) && totalAmmoAmmount > 0)
        {
            totalAmmoAmmount = totalAmmoAmmount + ammoInWeapon;
            totalAmmoAmmount = totalAmmoAmmount - 15;
            ammoInWeapon = maxAmmo;
        }*/
    }

    /// <summary>
    /// Ensures the player can shoot if the conditions are met
    /// </summary>
    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoInWeapon >= 1 && timerIsOn == false)
        {
            ammoInWeapon--;
            GameObject newBullet = Instantiate(bullet, spawnPosition.position, spawnPosition.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammoInWeapon <= 0)
        {
            Debug.Log("You don't have any ammo left!");
        }
    }

    /// <summary>
    /// Checks if the player can reload and acts accordingly
    /// </summary>
    public void Reload()
    {
        Debug.Log("Timer is running");
        timer = timer + Time.deltaTime;

        if (timer >= timerLimit)
        {
            ammoInWeapon = maxAmmo;
            timerIsOn = false;
            timer = 0; // Resets the timer
        }

        ReloadCooldownTimer();
    }

    private void ReloadCooldownTimer()
    {
        if (timerIsOn == false)
        {
            ReloadCooldownSlider.value = timerLimit;
        }

        if (timerIsOn == true)
        {
            ReloadCooldownSlider.value = timer;
        }
    }
}

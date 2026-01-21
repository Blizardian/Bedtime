using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShootMechanic : MonoBehaviour
{
    // For the inspector
    public GameObject Camera;
    public GameObject bullet;
    public Transform spawnPosition;
    public TMP_Text AmmountOfAmmoText;

    // Timer Related
    public float timer;
    private float timerLimit;
    public bool timerIsOn;

    // Cooldown
    public Slider ReloadCooldownSlider;

    // Ammo Related
    [SerializeField] private int ammoInWeapon;
    //[Range(0, 1000)][SerializeField] private int totalAmmoAmmount;
    [SerializeField] private int maxAmmo;

    // Weapon related
    public bool hasWeapon;
    public GameObject weaponPickup;
    public GameObject weaponPlayer;
    public GameObject weaponUI;
    void Start()
    {
        timerLimit = 3; //  Sets the timer limit

        spawnPosition = Camera.transform.Find("BulletSpawn"); // Sets the spawn position

        //totalAmmoAmmount = 45; // Sets the total ammount of ammo
        maxAmmo = 15; // Sets the max ammo for the gun (1 magazine)
        ammoInWeapon = maxAmmo; // Sets the ammo for the weapon equal to the max ammo, so you begin with a full clip

        AssignForgottenAtStart();
        ReloadCooldownTimer();

        weaponPickup = GameObject.Find("WeaponPickup");
        weaponPlayer = GameObject.Find("BulletSpawn");
        weaponUI = GameObject.Find("WeaponStats");

        DisableWeapon();

    }

    void Update()
    {
        if (hasWeapon)
        {
            if (timerIsOn == true)
            {
                Reload();
            }

            Shooting();

            RunTimerReload();

            ShowAmmoInWeapon();

            EnablePlayerGun();
        }

    }

    /// <summary>
    /// Disables the weapon and UI for the weapon
    /// </summary>
    private void DisableWeapon()
    {
        weaponPlayer.SetActive(false); // Set the gun inactive
        weaponUI.SetActive(false); // Disable the weaponUI
    }

    /// <summary>
    /// Sets forgotten values at the start
    /// </summary>
    private void AssignForgottenAtStart()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera");
            Debug.Log("INFO: Player has been automatically set");
        }
    }


    /// <summary>
    /// Enables the gun and UI of the gun
    /// </summary>
    private void EnablePlayerGun()
    {
        weaponPlayer.SetActive(true);
        weaponUI.SetActive(true);
    }

    /// <summary>
    /// Shows the ammount of ammo in the UI
    /// </summary>
    private void ShowAmmoInWeapon()
    {
        AmmountOfAmmoText.text = ammoInWeapon + " / " + maxAmmo;
    }

    /// <summary>
    /// Enables the timer so the player can reload
    /// </summary>
    private void RunTimerReload()
    {
        //Reload Mechanic if we have infinite ammo
        if (Input.GetKeyDown(KeyCode.R) && timerIsOn == false && ammoInWeapon != 15)
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

    /// <summary>
    /// Updates the UI of the reload timer (It updates the slider correctly)
    /// </summary>
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Watergun"))
        {
            hasWeapon = true;
            Destroy(collision.gameObject);
        }
    }
}

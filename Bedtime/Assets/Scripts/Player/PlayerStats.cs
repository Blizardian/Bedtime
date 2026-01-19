using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Singleton Related
    public static PlayerStats Instance;

    // HP Related
    public int HP;
    public int MaxHP = 100;

    // UI
    [Tooltip("This needs to be assigned manually!")]
    public Slider healthBar; // Set in the inspector
    private void Awake()
    {
        SetMaxHealth();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void SetMaxHealth()
    {
        HP = MaxHP; Debug.Log("Player's HP is " + HP + " at start"); // Ensures the HP is full at the start
    }

    void Update()
    {
        HPLimiter();

        HealthUpdater();

        HealthChecker();
    }

    /// <summary>
    /// Checks if the player should be dead or not
    /// </summary>
    private void HealthChecker()
    {
        if (HP <= 0)
        {
            Debug.Log("Player has died");
        }
    }

    /// <summary>
    /// Updates the HP in the UI
    /// </summary>
    private void HealthUpdater()
    {
        healthBar.value = HP; // Update the HP
    }

    /// <summary>
    /// Limits the HP to the MAX HP, so that it can't succeed the MaxH
    /// </summary>
    private void HPLimiter()
    {
        if (HP > MaxHP)
        {
            HP = MaxHP;
        }
    }
}

using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    // Singleton Related
    public static PlayerStats Instance;

    // HP Related
    public int HP;
    public int MaxHP = 100;

    // UI
    public TMP_Text HP_UI_Text;
    private void Awake()
    {
        HP = MaxHP; Debug.Log("Player's HP is " + HP + " at start");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        HPLimiter();

        HPTextUpdater();

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
    private void HPTextUpdater()
    {
        HP_UI_Text.text = "HP: " + HP;
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

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Singleton Related
    public static PlayerStats Instance;

    // HP Related
    public int HP;
    public int MaxHP = 100;

    // Score related
    public int playerScore;
    public int scoreReceivedOnKill;
    public int scoreNeededLevel1;
    public int scoreNeededLevel2;
    //public int scoreNeededLevel3;
    public int StageTracker;
    [Tooltip("This needs to be assigned manually!")]
    public TMP_Text scoreText;

    // UI
    [Tooltip("This needs to be assigned manually!")]
    public Slider healthBar; // Set in the inspector

    // Door
    [Tooltip("This needs to be assigned manually!")]
    GameObject door;
    Animator doorAnimator;

    private void Awake()
    {
        scoreReceivedOnKill = 50;
        scoreNeededLevel1 = 300;
        scoreNeededLevel2 = 500;
        StageTracker = 1;
        SetMaxHealth();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        door = GameObject.Find("Door");
        doorAnimator = door.GetComponent<Animator>();

    }

    void Update()
    {
        HPLimiter();

        HealthUpdater();

        HealthChecker();

        CompletedLevel();
        
    }

    /// <summary>
    /// Shows the score and score needed for level 1
    /// </summary>
    private void ShowScoreLevel1()
    {
        scoreText.text = "Score: " + playerScore + "/ " + scoreNeededLevel1; // Updates the scoreText with the player's score and the score that is needed
    }
    /// <summary>
    /// Shows the score and score needed for level 2
    /// </summary>
    private void ShowScoreLevel2()
    {
        scoreText.text = "Score: " + playerScore + "/ " + scoreNeededLevel2; // Updates the scoreText with the player's score and the score that is needed
    }

    /// <summary>
    /// Checks if the player should be dead or not
    /// </summary>
    private void HealthChecker()
    {
        if (HP <= 0)
        {
            Debug.Log("Player has died");
            ButtonMethods.conditionText = "You lost!";
            SceneManager.LoadScene(1);
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

    /// <summary>
    /// Sets the HP of the player to the MaxHP value (So the player gets fully healed)
    /// </summary>
    private void SetMaxHealth()
    {
        HP = MaxHP; Debug.Log("Player's HP is " + HP + " at start"); // Ensures the HP is full at the start
    }

    public void OpenDoor()
    {
        doorAnimator.enabled = true;
    }

    /// <summary>
    /// Marks the level as completed
    /// </summary>
    private void CompletedLevel()
    {
            switch (StageTracker)
            {
                case 1:
                ShowScoreLevel1();

                if (playerScore == scoreNeededLevel1)
                {
                    StageTracker = 2;
                    playerScore = 0;
                    //ButtonMethods.conditionText = "You Won!";
                    //SceneManager.LoadScene(2);
                }
                break;
                    
            case 2:
                ShowScoreLevel2();
                OpenDoor();
                break;
            }
        }
}

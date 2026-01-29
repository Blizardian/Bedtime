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
    public int scoreNeededLevel3;
    public int StageTracker;
    [Tooltip("This needs to be assigned manually!")]
    public TMP_Text scoreText;

    // UI
    [Tooltip("This needs to be assigned manually!")]
    public Slider healthBar; // Set in the inspector

    // Door
    [Tooltip("This needs to be assigned manually!")]
    GameObject door02;
    GameObject door03;
    Animator doorAnimator02;
    Animator doorAnimator03;

    //Enemies
    public GameObject batch1;
    public GameObject batch2;
    public GameObject batch3;

    // Light
    public GameObject endLight;

    private void Awake()
    {
        scoreReceivedOnKill = 50;
        scoreNeededLevel1 = 750; // 750
        scoreNeededLevel2 = 300; // 350
        scoreNeededLevel3 = 200; // 200
        StageTracker = 1;
        SetMaxHealth();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        batch1 = GameObject.Find("Batch1");
        batch1.SetActive(false);
        batch2 = GameObject.Find("Batch2");
        batch2.SetActive(true);
        batch2 = GameObject.Find("Batch3");

        door02 = GameObject.Find("Door");
        door03 = GameObject.Find("Door03");
        doorAnimator02 = door02.GetComponent<Animator>();
        doorAnimator03 = door03.GetComponent<Animator>();
        endLight = GameObject.Find("EndTrigger");
        endLight.SetActive(false);

    }

    void Update()
    {
        HPLimiter();

        HealthUpdater();

        HealthChecker();

        CompletedLevel();

        QuickReturnToMainMenu();
    }

    private static void QuickReturnToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); // Return to the main menu
            PlayerMovement.UnlockCursor();
        }
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
    /// Shows the score and score needed for level 3
    /// </summary>
    private void ShowScoreLevel3()
    {
        scoreText.text = "Score: " + playerScore + "/ " + scoreNeededLevel3; // Updates the scoreText with the player's score and the score that is needed
    }

    private void VoidHitter()
    {
        SceneManager.LoadScene(2); // Load the level
    }

    /// <summary>
    /// Shows no score
    /// </summary>
    private void ShowNone()
    {
        scoreText.text = "Go through the light! "; // Updates the scoreText with the player's score and the score that is needed
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
            SceneManager.LoadScene(2); // Load the level
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
    /// <summary>
    /// Opens the second door
    /// </summary>
    public void OpenDoor02()
    {
        doorAnimator02.enabled = true;
    }

    /// <summary>
    /// Opens the third door
    /// </summary>
    public void OpenDoor03()
    {
        doorAnimator03.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "EndTrigger")
        {
            SceneManager.LoadScene(3); // Load the end scene
        }

        if (other.gameObject.name == "Stage3Trigger" && StageTracker == 2)
        {
            StageTracker = 3;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Stage5Trigger" && StageTracker == 4)
        {
            StageTracker = 5;
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "Void")
        {
            VoidHitter();
        }
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

                if (playerScore >= scoreNeededLevel1)
                {
                    StageTracker = 2;
                    playerScore = 0;
                    ButtonMethods.conditionText = "You Won!";
                }
                break;
                    
            case 2:
                playerScore = 0;
                ShowScoreLevel2();
                OpenDoor02();
                batch3.SetActive(false);
                break;
            case 3:
                ShowScoreLevel2();

                batch1.SetActive(true);

                if (playerScore >= scoreNeededLevel2)
                {
                    StageTracker = 4;
                }
                    break;

            case 4:
                ShowScoreLevel3();
                batch1.SetActive(false);
                playerScore = 0;
                OpenDoor03();
                    break;

            case 5:
                ShowScoreLevel3();
                batch2.SetActive(true);
                if (playerScore >= scoreNeededLevel3)
                {
                    StageTracker = 6;
                }
                break;

            case 6:
                ShowNone();
                batch2.SetActive(false);
                endLight.SetActive(true);
                break;
            }
        }
}

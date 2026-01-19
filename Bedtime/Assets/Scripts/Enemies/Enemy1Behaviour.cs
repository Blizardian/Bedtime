using UnityEngine;
using UnityEngine.UI;

public class Enemy1Behaviour : MonoBehaviour
{
    public int enemy1Health;
    public Transform target;
    [SerializeField] private float explodeRange;

    // Timer
    [SerializeField] float explodeTimer;
    [SerializeField] float explodeTimerMax;
    [SerializeField] bool explodeTimerIsOn;

    // Show Range
    [SerializeField] GameObject explodeRangePrefab;
    GameObject showExplodeRange;
    [SerializeField] float showRange;
    [SerializeField] float preventExplosionRange;

    //HealthBar
    [Tooltip("This needs to be assigned manually!")]
    public Slider healthBar; // Set in the inspector
    [Tooltip("This needs to be assigned manually!")]
    public GameObject HealthBarObject; // Set in the inspector
    void Start()
    {
        SetIfZero();

        // Instantiate a unique circle for this enemy
        showExplodeRange = Instantiate(explodeRangePrefab, transform.position, Quaternion.identity);
        showExplodeRange.transform.SetParent(transform); // So it moves with the enemy
    }

    /// <summary>
    /// Sets some value's or inspector related stuff if they aren't set (So the game won't break)
    /// </summary>
    private void SetIfZero()
    {
        if (explodeTimer == 0)
        {
            explodeTimerMax = 1;
        }
        if (showRange == 0)
        {
            showRange = 8;
        }
        if (explodeTimer == 0)
        {
            explodeRange = 3;
        }

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        if (enemy1Health == 0)
        {
            enemy1Health = 100;
        }

        preventExplosionRange = 6;

    }

    void Update()
    {
        UpdateEnemyHealth();

        if (enemy1Health <= 0)
        {
            Destroy(gameObject);
        }

        if (target)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance < explodeRange)
            {
                if (!explodeTimerIsOn)
                {
                    explodeTimerIsOn = true;

                    Debug.Log("Explosion incoming");
                }
            }

            ActivateExplodeTimer();

            if (explodeTimer >= explodeTimerMax)
            {
                if (distance > preventExplosionRange)
                {
                    Debug.Log("Stopped the explosion");
                    ResetExplodeTimer();
                }
                else
                {
                    if (distance <= explodeRange)
                    {
                        Debug.Log("Player will get damage");
                        PlayerStats.Instance.HP -= 50;
                    }
                    else
                    {
                        Debug.Log("Player will not get damage");
                    }

                    Destroy(gameObject);
                }
            }

            if (distance < showRange)
            {
                ShowExplodeRangeInGame();
                HealthBarObject.SetActive(true);
            }
            else
            {
                showExplodeRange.SetActive(false);
                HealthBarObject.SetActive(false);
            }
        }
    }

    private void UpdateEnemyHealth()
    {
        healthBar.value = enemy1Health;
    }

    /// <summary>
    /// Resets the timer of the explosion
    /// </summary>
    private void ResetExplodeTimer()
    {
        explodeTimerIsOn = false;
        explodeTimer = 0;
    }

    /// <summary>
    /// Checks if the Explosion timer should be counting or not (This is set in update)
    /// </summary>
    private void ActivateExplodeTimer()
    {
        if (explodeTimerIsOn == true)
        {
            explodeTimer += Time.deltaTime;
        }
        else
        {
            explodeTimer = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Changes the color
        Gizmos.DrawWireSphere(transform.position, explodeRange); // Draws a sphere of on the position and range of explodeRange for the object the script is attached to.
        Gizmos.DrawWireSphere(transform.position, preventExplosionRange);
    }

    /// <summary>
    /// Shows the circle ingame and scales it correctly
    /// </summary>
    void ShowExplodeRangeInGame()
    {
        Vector3 correctScale = new Vector3(explodeRange * 2, 0.0001f, explodeRange * 2);
        Vector3 position = new Vector3(0, -1.08f, 0);
        showExplodeRange.transform.localPosition = position;
        showExplodeRange.transform.localScale = correctScale;
        showExplodeRange.SetActive(true);
    }
}

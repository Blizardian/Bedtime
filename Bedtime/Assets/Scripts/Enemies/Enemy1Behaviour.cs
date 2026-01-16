using UnityEngine;

public class Enemy1Behaviour : MonoBehaviour
{
    public int enemy1Health;
    public Transform target;
    [SerializeField] private float explodeRange;

    // Timer
    [SerializeField] float explodeTimer;
    [SerializeField] float explodeTimerMax;
    [SerializeField] bool explodeTimerIsOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        explodeTimerMax = 1;
        explodeRange = 3;
        target = GameObject.FindWithTag("Player").transform;
        enemy1Health = 100;
    }

    // Update is called once per frame
    void Update()
    {
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
}

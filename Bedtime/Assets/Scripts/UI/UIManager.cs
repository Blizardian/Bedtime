using UnityEngine;
using System.Collections;
public class UIManager : MonoBehaviour
{
    // Singleton Related
    public static UIManager Instance;

    // GameObjects
    public GameObject hitIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator HitIndicatorTimer()
    {
        hitIndicator.SetActive(true);

        float timer = 0f;
        float timerMax = 1f;

        while (timer < timerMax)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        hitIndicator.SetActive(false);
    }

    public void ShowHitIndicator()
    {
        StartCoroutine(HitIndicatorTimer());
    }
}

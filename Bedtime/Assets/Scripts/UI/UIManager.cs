using UnityEngine;
using System.Collections;
public class UIManager : MonoBehaviour
{
    // Making it accessible
    public static UIManager Instance;

    // GameObjects
    public GameObject hitIndicator;
    public GameObject healIndicator;
    public GameObject TutorialPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(TutorialPanel == null)
        {
            Debug.Log("TutorialPanel Has not been set");
        }

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

    public void OpenTutorialPanel()
    {
        TutorialPanel.SetActive(true);
    }

    public void CloseTutorialPanel()
    {
        TutorialPanel.SetActive(false);
    }

    IEnumerator HitIndicatorTimer()
    {
        hitIndicator.SetActive(true);

        float timer = 0f;
        float timerMax = 0.5f;

        while (timer < timerMax)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        hitIndicator.SetActive(false);
    }

    IEnumerator HealIndicatorTimer()
    {
        healIndicator.SetActive(true);

        float timer = 0f;
        float timerMax = 0.5f;

        while (timer < timerMax)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        healIndicator.SetActive(false);
    }

    public void ShowHitIndicator()
    {
        StartCoroutine(HitIndicatorTimer());
    }

    public void ShowHealIndicator()
    {
        StartCoroutine(HealIndicatorTimer());
    }
}

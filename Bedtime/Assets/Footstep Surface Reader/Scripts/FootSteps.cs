using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootSteps : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Range(0f, 20f)]
    [SerializeField] float frequency = 10.0f;
    [SerializeField] UnityEvent onFootStep;
    float Sin;
    bool isTriggered = false;

    void Update()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        if (inputMagnitude > 0)
        { StartFootsteps(); }
    }
    private void StartFootsteps()
    {
        Sin = Mathf.Sin(Time.time * frequency);
        if (Sin > 0.97f && isTriggered == false)
        {
            isTriggered = true;
            Debug.Log("Tic");
            onFootStep.Invoke();
        }
        else if (isTriggered == true && Sin < 0.97f)
        {
            isTriggered = false;
        }
    }
}

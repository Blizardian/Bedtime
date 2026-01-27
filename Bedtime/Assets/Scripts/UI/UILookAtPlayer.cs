using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UILookAtPlayer : MonoBehaviour
{
    public GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform); // Always look at the player
    }
}

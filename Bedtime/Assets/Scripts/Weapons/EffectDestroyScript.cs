using UnityEngine;

public class EffectDestroyScript : MonoBehaviour
{
    private float seconds;
    // The purpose of this script is destroying the effect when it is created
    void Start()
    {
        seconds = 2;
        Destroy(gameObject, seconds); // Destroy the object after ammount of seconds
    }
}

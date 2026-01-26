using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private void Update()
    {
        SelfDamageTest();
    }

    /// <summary>
    /// If you press F you can damage yourself
    /// </summary>
    private static void SelfDamageTest()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("You gave yourself damage");
            PlayerStats.Instance.HP -= 25; // Damage yourself for a value
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthPickup(collision);
    }

    /// <summary>
    /// Gives health on collision and destroys the object
    /// </summary>
    /// <param name="collision"></param>
    private static void HealthPickup(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            PlayerStats.Instance.HP += 25;
            UIManager.Instance.ShowHealIndicator(); // Notify the player that he received health
            Destroy(collision.gameObject);
        }
    }
}

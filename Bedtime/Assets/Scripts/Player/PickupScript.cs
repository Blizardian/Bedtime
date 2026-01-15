using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("You gave yourself damage");
            PlayerStats.Instance.HP -= 25;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("HealthPickup"))
        {
            PlayerStats.Instance.HP += 25;
            Destroy(collision.gameObject);
        }
    }
}

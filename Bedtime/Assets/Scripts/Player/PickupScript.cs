using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerStats.Instance.HP);
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

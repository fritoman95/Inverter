using UnityEngine;

public class ItemHealingPickup : MonoBehaviour
{
    [SerializeField]
    private float healing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float maxhealth = collision.gameObject.GetComponent<PlayerHealth>().maxHealth;

        PlayerHealth healingComponent = collision.gameObject.GetComponent<PlayerHealth>();

        if(collision.gameObject.GetComponent<PlayerHealth>().currentHealth != maxhealth && collision.gameObject.CompareTag("Player"))
        {
            healingComponent.HealthChange(-healing);

            Destroy(gameObject);
        }
    }
}
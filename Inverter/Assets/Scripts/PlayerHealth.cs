using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //[SerializeField]
    public float maxHealth = 100;
    //[SerializeField]
    public float currentHealth;

    [SerializeField]
    private PlayerUI healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void HealthChange(float amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            if(gameObject.layer == 11)
            {
                Debug.Log("Enemy killed");
            }

            else if(gameObject.layer == 10)
            {
                Debug.Log("Player killed");
            }

            if (this.gameObject.CompareTag("Player"))
            {
                this.gameObject.transform.DetachChildren();
            }

            Destroy(this.gameObject);
        }
    }
}
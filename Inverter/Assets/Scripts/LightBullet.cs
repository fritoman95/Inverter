using UnityEngine;

public class LightBullet : MonoBehaviour
{
    private GameObject player;

    private float bulletDmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.Find("Player");

        bulletDmg = player.GetComponentInChildren<PlayersGun>().bulletDamage;

        EnemyHealth healthComponent = collision.gameObject.GetComponent<EnemyHealth>();

        var HealthParticleSystem = collision.gameObject.GetComponent<HealthChangeParticles>();


        if (healthComponent && collision.gameObject.tag != "DarkEnemy" && collision.gameObject.tag != "LightEnemy")
        {
            healthComponent.HealthChange(-bulletDmg);

            Destroy(gameObject);

            HealthParticleSystem.PlayDamageParticles();

            return;
        }

        if (healthComponent && collision.gameObject.CompareTag("DarkEnemy"))
        {
            healthComponent.HealthChange(-bulletDmg * 2);

            Destroy(gameObject);

            HealthParticleSystem.PlayDamageParticles();

            return;
        }
        
        if (healthComponent && collision.gameObject.CompareTag("LightEnemy"))
        {
            healthComponent.HealthChange(-bulletDmg / 2);

            Destroy(gameObject);

            HealthParticleSystem.PlayDamageParticles();

            return;
        }
    }
}
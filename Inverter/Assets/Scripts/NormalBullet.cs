using UnityEngine;

public class NormalBullet : MonoBehaviour
{
    private GameObject player;
    
    private float bulletDmg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = GameObject.Find("Player");

        bulletDmg = player.GetComponentInChildren<PlayersGun>().bulletDamage;

        EnemyHealth healthComponent = collision.gameObject.GetComponent<EnemyHealth>();

        var HealthParticleSystem = collision.gameObject.GetComponent<HealthChangeParticles>();


        if (healthComponent)
        {
            healthComponent.HealthChange(-bulletDmg);

            Destroy(gameObject);

            HealthParticleSystem.PlayDamageParticles();
        }
    }
}
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField]
    private int Damage;

    PlayerHealth HealthDamage;

    private GameObject player;
    [SerializeField]
    private float attackSpeedMax;
    [SerializeField]
    private float attackSpeedCur;

    

    private void Start()
    {
        player = GameObject.Find("Player");

        HealthDamage = player.gameObject.GetComponent<PlayerHealth>();

        attackSpeedCur = attackSpeedMax;
    }

    private void Update()
    {
        AttackEnemy();
    }

    // change melee attack later to make it so it attacks along with the animation and not with a timer
    private void AttackEnemy()
    {
        var playerInRange = gameObject.GetComponent<EnemyMovement>().inAttackRange;

        if(playerInRange)
        {
            attackSpeedCur -= Time.deltaTime * 1;

            if(attackSpeedCur <= 0)
            {
                HealthDamage.HealthChange(-Damage);

                player.GetComponent<HealthChangeParticles>().PlayDamageParticles();

                attackSpeedCur = attackSpeedMax;
            }
        }

        else
        {
            attackSpeedCur = attackSpeedMax;
        }
    }
}

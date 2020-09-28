using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private Transform playerPos;

    //chase radius that checks for player
    [SerializeField]
    private float chaseRadius;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private Vector2 movement;
    [SerializeField]
    private float moveSpeed;

    public bool isInRange;

    public bool inAttackRange;

    //inner radius check
    [SerializeField]
    private float innerRadius;

    //if the player shoots the enemy while outside the radius chase for a set time
    [SerializeField]
    private bool chaseOnShot;
    [SerializeField]
    private float chaseTimerMax;
    [SerializeField]
    private float chaseTimerMin;


    //commented out just incase needed later
    /*private Collider2D outterRadiusCheck;
    private Collider2D innerRadiusCheck;*/

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //if enemies are on kinematic setting they work and dont have problems but can overlap each other
        //dynamic enemies constantly push and jump towards player when in chase range but dont overlap

        //possibly just install A* for enemy movement mesh in 2D to fix problems and not have enemies push each other while in movement range
        if(Vector2.Distance(transform.position, playerPos.position) <= chaseRadius && Vector2.Distance(transform.position, playerPos.position) > innerRadius)
        {
            isInRange = true;
        }

        else
        {
            isInRange = false;
        }

        Vector2 direction = (playerPos.position - transform.position).normalized;

        movement = direction;

        //checks to see if player is in the innerradius
        if(Vector2.Distance(transform.position, playerPos.position) <= innerRadius)
        {
            inAttackRange = true;
        }

        else
        {
            inAttackRange = false;
        }
    }

    private void FixedUpdate()
    {
        if(isInRange)
        {
            ChasePlayer(movement);

            chaseOnShot = false;
        }


        if (chaseOnShot && Vector2.Distance(transform.position, playerPos.position) > innerRadius)
        {
            ChasePlayer(movement);
            ChaseTimer();
        }

        rb.velocity = Vector2.zero;
    }

    private void ChasePlayer(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void ChaseTimer()
    {
        if (chaseTimerMin > 0)
        {
            chaseTimerMin -= Time.deltaTime * 1;

            if (chaseTimerMin <= 0)
            {
                chaseOnShot = false;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            chaseOnShot = true;

            chaseTimerMin = chaseTimerMax;

            chaseTimerMin -= Time.deltaTime * 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}

using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public bool canControl = true;

    public float speed;
    public Rigidbody2D rb;
    private SpriteRenderer sprite;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //calls code to flip the character
        FlipCharacter();
    }

    private void FixedUpdate()
    {
        //calls to move character in fixedUpdate
        MoveCharacter();
    }
    
    //Basic character movement code
    public void MoveCharacter()
    {
        if (canControl == true)
        {
            var isDashing = GetComponent<PlayerDash>();

            if(isDashing.dashPressed == false)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
            }

            else if(isDashing.dashPressed == true)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    //flips the character and all it is holding
    void FlipCharacter()
    {
        if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }

        else if(rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }
    }
}
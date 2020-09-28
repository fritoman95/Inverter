using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //probably should put dash in its own script so I dont put to much in one script... but who knows.
    [Header("Dash")]
    public float dashTime;
    [SerializeField]
    private float dashDist;
    public float dashRadius;
    private bool canDash = true;
    public bool dashPressed = false;

    private Vector3 targetPosition;
    private Vector3 mousepos;

    public int maxDashCount = 3;

    public int currentDashCount;

    [SerializeField]
    public float maxTimer;
    [SerializeField]
    public float currentTimer;

    private TrailRenderer trail;

    [SerializeField]
    private DashUI dash;


    private void Start()
    {
        currentDashCount = maxDashCount;

        //currentTimer = maxTimer;

        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //sets up the mouse to be in relation to what the camera see's
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //dash radius
        dashDist = Vector2.Distance(transform.position, mousepos);

        Vector3 mousePosition = new Vector3(mousepos.x, mousepos.y, 0);
        Vector3 direction = mousePosition - transform.position;


        //if requirements met, starts dash code
        if (currentDashCount > 0)
        {
            if (dashDist <= dashRadius)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
                {
                    targetPosition = new Vector3(mousepos.x, mousepos.y, 0);

                    dashPressed = true;
                    canDash = false;
                }

                //calls for dash movement code
                if (dashPressed == true)
                {
                    Playerdash();
                }
            }


            else if (dashDist > dashRadius)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
                {
                    targetPosition = transform.position + direction.normalized * dashRadius;

                    dashPressed = true;
                    canDash = false;
                }

                //calls for dash movement code
                if (dashPressed == true)
                {
                    Playerdash();
                }
            }
        }
        DashTimer();
    }

    //Dash movement code
    public void Playerdash()
    {
        trail.enabled = true;
        Physics2D.IgnoreLayerCollision(10, 11);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, dashTime * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            dashPressed = false;
            canDash = true;

            currentDashCount = currentDashCount - 1;

            Physics2D.IgnoreLayerCollision(10, 11, false);

            trail.enabled = false;

            dash.DecreaseDash();
        }
    }

    private void DashTimer()
    {
        if(currentDashCount < maxDashCount)
        {
            if(currentTimer >= 0)
            {
                currentTimer += 1 * Time.deltaTime;

                if(currentTimer >= maxTimer)
                {
                    currentTimer = 0;

                    currentDashCount = currentDashCount + 1;

                    dash.IncreaseDash();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, dashRadius);
    }
}
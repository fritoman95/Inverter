using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool gunIsHeld;

    [SerializeField]
    private Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20;

    //gun rotation;
    private Rigidbody2D rb;
    Vector2 mousePos;
    [SerializeField]
    private Transform playerPos;

    public float FireRateMax;
    public float FireRateMin;

    private void Start()
    {
        firePoint = GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody2D>();

        FireRateMin = FireRateMax;
    }


    private void Update()
    {
        //checks that the gun is being held by the player;
        IsHeld();

        if (gunIsHeld)
        {
            transform.position = playerPos.position;

            //start calling for the rotation of the gun before shooting
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if left click start shooting
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    void Shoot()
    {
        FireRateMin -= Time.deltaTime * 1;

        if(FireRateMin < 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

            FireRateMin = FireRateMax;
        }

    }

    void IsHeld()
    {
        if (gameObject.transform.parent == null)
        {
            gunIsHeld = false;

            rb.isKinematic = true;
        }
        
        else if(gameObject.transform.parent.CompareTag("Player"))
        {
            gunIsHeld = true;

            rb.isKinematic = false;
        }
    }
}

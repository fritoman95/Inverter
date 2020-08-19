using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool gunIsHeld;

    [SerializeField]
    private Transform firePoint;
    public GameObject normalBulletPref;
    public GameObject darkBulletPref;
    public GameObject lightBulletPref;
    public float bulletForce = 20;

    //bullet types
    [SerializeField]
    private bool isNormal;
    [SerializeField]
    private bool isDark;
    [SerializeField]
    private bool isLight;

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

        isNormal = true;
    }


    public void Update()
    {
        //checks that the gun is being held by the player;
        IsHeld();

        if(gunIsHeld)
        {
            if (Input.GetMouseButtonDown(1))
            {
                AmmoTypeSwitch();
            }
        }

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

    public void Shoot()
    {
        if(isNormal == true)
        {
            if (FireRateMin == FireRateMax)
            {
                GameObject bullet = Instantiate(normalBulletPref, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                FireRateMin -= Time.deltaTime * 1;
            }

            else if (FireRateMin != FireRateMax)
            {
                FireRateMin -= Time.deltaTime * 1;

                if (FireRateMin <= 0)
                {
                    FireRateMin = FireRateMax;
                }
            }
        }

        else if(isDark == true)
        {
            if (FireRateMin == FireRateMax)
            {
                GameObject bullet = Instantiate(darkBulletPref, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                FireRateMin -= Time.deltaTime * 1;
            }

            else if (FireRateMin != FireRateMax)
            {
                FireRateMin -= Time.deltaTime * 1;

                if (FireRateMin <= 0)
                {
                    FireRateMin = FireRateMax;
                }
            }
        }

        else if(isLight == true)
        {
            if (FireRateMin == FireRateMax)
            {
                GameObject bullet = Instantiate(lightBulletPref, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                FireRateMin -= Time.deltaTime * 1;
            }

            else if (FireRateMin != FireRateMax)
            {
                FireRateMin -= Time.deltaTime * 1;

                if (FireRateMin <= 0)
                {
                    FireRateMin = FireRateMax;
                }
            }
        }
    }

    void AmmoTypeSwitch()
    {
        if (isNormal == true)
        {
            isNormal = false;
            isDark = true;
            isLight = false;

            Debug.Log("Using Dark Ammo");
        }

        else if (isDark == true)
        {
            isNormal = false;
            isDark = false;
            isLight = true;

            Debug.Log("Using light Ammo");
        }

        else if (isLight == true)
        {
            isNormal = true;
            isDark = false;
            isLight = false;

            Debug.Log("Using normal Ammo");
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

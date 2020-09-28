using UnityEngine;

public class PlayersGun : MonoBehaviour
{
    [SerializeField]
    private bool gunIsHeld;

    //position the bullets spawn at
    [SerializeField]
    private Transform firePoint;

    //different bullet prefabs
    public GameObject normalBulletPref;
    public GameObject darkBulletPref;
    public GameObject lightBulletPref;


    //speed of the bullet
    [SerializeField]
    private float bulletForce;

    //damage duh
    public float bulletDamage;

    //bullet types ( possibly change to an enum when I FUCKING LEARN WHAT ENUMS ARE)
    [SerializeField]
    private bool isNormal;
    [SerializeField]
    private bool isDark;
    [SerializeField]
    private bool isLight;

    //gun rotation;
    private Rigidbody2D rb;
    private Vector2 mousePos;
    [SerializeField]
    private Transform playerPos;

    //fire rate speed
    [SerializeField]
    private float FireRateMax;
    [SerializeField]
    private float FireRateMin;

    private bool gunCoolDown;

    [SerializeField]
    private float gunCoolDownTimerMax;
    [SerializeField]
    private float gunCoolDownTimerMin;

    [SerializeField]
    private WeaponUnlock weaponUnlock;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        FireRateMin = FireRateMax;

        isNormal = true;

        gunCoolDownTimerMin = gunCoolDownTimerMax;
    }


    public void Update()
    {
        //checks that the gun is being held by the player;
        IsHeld();

        if(gunIsHeld)
        {
            transform.position = playerPos.position;

            //start calling for the rotation of the gun before shooting
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //if left click start shooting
            if (Input.GetMouseButton(0) && gunCoolDown == false)
            {
                Shoot();
            }

            else if(Input.GetMouseButtonUp(0))
            {
                gunCoolDown = true;
            }

            if(gunCoolDown)
            {
                GunCoolDown();
            }
            

            //switch ammo type
            if (Input.GetMouseButtonDown(1))
            {
                AmmoTypeSwitch();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }


    //shooting method that checks the weapon type(dark, light or normal) and changes the bulletprefabs according to the type
    public void Shoot()
    {
        Debug.Log("Dark Ammo: " + weaponUnlock.DarkAmmo + ", Light Ammo " + weaponUnlock.LightAmmo);

        if(isNormal == true)
        {
            if (FireRateMin == FireRateMax)
            {
                GameObject bullet = Instantiate(normalBulletPref, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                FireRateMin -= Time.deltaTime * 1;

                Destroy(bullet, 5);
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

        else if(isDark == true && weaponUnlock.DarkAmmo > 0)
        {
            if (FireRateMin == FireRateMax)
            {
                GameObject bullet = Instantiate(darkBulletPref, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                weaponUnlock.DarkAmmo--;

                FireRateMin -= Time.deltaTime * 1;

                Destroy(bullet, 5);
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

        else if(isLight == true && weaponUnlock.LightAmmo > 0)
        {
            if (FireRateMin == FireRateMax)
            {
                GameObject bullet = Instantiate(lightBulletPref, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

                weaponUnlock.LightAmmo--;


                FireRateMin -= Time.deltaTime * 1;

                Destroy(bullet, 5);
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

        if(weaponUnlock.DarkAmmo <= 0 || weaponUnlock.LightAmmo <= 0)
        {
            Debug.Log("Dont have enough ammo");
        }
    }


    //helps stop the player from tapping the shoot button and restarts the cooldown after letting go of each shot
    void GunCoolDown()
    {
        if(gunCoolDown)
        {
            gunCoolDownTimerMin -= Time.deltaTime * 1;

            if (gunCoolDownTimerMin != gunCoolDownTimerMax)
            {
                gunCoolDownTimerMin -= Time.deltaTime * 1;

                if (gunCoolDownTimerMin <= 0)
                {
                    gunCoolDown = false;

                    gunCoolDownTimerMin = gunCoolDownTimerMax;

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
        if (gameObject.transform.parent.tag != "Player")
        {
            gunIsHeld = false;

            rb.isKinematic = true;
        }
        
        else if(gameObject.transform.parent.tag == "Player")
        {
            gunIsHeld = true;

            rb.isKinematic = false;
        }
    }
}
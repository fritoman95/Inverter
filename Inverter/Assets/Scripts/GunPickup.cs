using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] private float weaponPickupRad;
    public LayerMask layer;

    private Collider2D weaponCheck;

    [SerializeField] private GameObject weaponInRange1, weaponInHand;
    private bool holdingWeapon;

    private void Update()
    {
        weaponCheck = Physics2D.OverlapCircle(transform.position, weaponPickupRad, layer);
        WeaponInRangeCheck();

        if (holdingWeapon == false && weaponInRange1 != null && Input.GetKeyDown(KeyCode.E))
        {
            WeaponPickup();
        }

        else if(holdingWeapon == true && Input.GetKeyDown(KeyCode.E))
        {
            WeaponDrop();
        }
    }


    void WeaponInRangeCheck()
    {
        if(weaponCheck)
        {
            weaponInRange1 = weaponCheck.gameObject;
        }

        else
        {
            weaponInRange1 = null;
        }
    }

    void WeaponPickup()
    {
        weaponInRange1.gameObject.transform.parent = gameObject.transform;
        holdingWeapon = true;
    }

    void WeaponDrop()
    {
        weaponInRange1.gameObject.transform.parent = null;
        holdingWeapon = false;
    }
}
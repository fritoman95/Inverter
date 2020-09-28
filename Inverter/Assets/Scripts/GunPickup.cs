using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField]
    private float weaponPickupRad;

    [SerializeField]
    public LayerMask layer;

    private Collider2D weaponCheck;

    [SerializeField]
    private GameObject weaponInRange1;
    
    [SerializeField]
    private bool holdingWeapon;

    private void Update()
    {
        weaponCheck = Physics2D.OverlapCircle(transform.position, weaponPickupRad, layer);
        WeaponInRangeCheck();

        if (holdingWeapon == false && weaponInRange1 != null && Input.GetKeyDown(KeyCode.E))
        {
            WeaponPickup();
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
        //weaponInHand = weaponInRange1;
        weaponInRange1.gameObject.transform.parent = gameObject.transform;
        holdingWeapon = true;

        Debug.Log("congrats you got a weapon");
    }
}
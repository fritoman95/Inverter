using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField]
    private WeaponUnlock weaponUnlock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(gameObject.CompareTag("DarkAmmoDrop"))
            {
                weaponUnlock.DarkAmmo += 5;

                Destroy(gameObject);
            }

            if(gameObject.CompareTag("LightAmmoDrop"))
            {
                weaponUnlock.LightAmmo += 5;

                Destroy(gameObject);
            }
        }
    }
}

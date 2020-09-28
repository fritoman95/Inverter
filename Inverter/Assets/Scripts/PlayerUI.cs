using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider slider;

    [SerializeField]
    private WeaponUnlock weaponUnlock;

    [SerializeField]
    private TextMeshProUGUI DarkAmmoCounter;
    [SerializeField]
    private TextMeshProUGUI LightAmmoCounter;

    private void Update()
    {
        DarkAmmoCounter.text = weaponUnlock.DarkAmmo.ToString();

        LightAmmoCounter.text = weaponUnlock.LightAmmo.ToString();
    }

    public void SetMaxHealth(float health)
    {
        health = GameObject.Find("Player").GetComponent<PlayerHealth>().maxHealth;

        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        health = GameObject.Find("Player").GetComponent<PlayerHealth>().currentHealth;

        slider.value = health;
    }
}
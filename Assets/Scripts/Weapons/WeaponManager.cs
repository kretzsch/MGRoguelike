using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponHolder; // The parent Transform where weapon instances will be created

    private Dictionary<WeaponData, ProjectileWeapon> weapons = new Dictionary<WeaponData, ProjectileWeapon>();

    public void SetupWeapons(Dictionary<WeaponData, int> weaponsAndAmmo)
    {
        foreach (KeyValuePair<WeaponData, int> weaponAndAmmo in weaponsAndAmmo)
        {
            WeaponData weaponData = weaponAndAmmo.Key;
            int weaponAmmo = weaponAndAmmo.Value;

            if (weaponData != null)
            {
                // Create a new GameObject with a SpriteRenderer
                GameObject weaponInstance = new GameObject(weaponData.weaponName + " Sprite");
                weaponInstance.AddComponent<SpriteRenderer>();
                weaponInstance.GetComponent<SpriteRenderer>().sprite = weaponData.weaponVisualsData.levelVisuals[0].sprite;

                // Set the weapon instance parent to the weaponHolder
                weaponInstance.transform.SetParent(weaponHolder);

                // Get the ProjectileWeapon component from the instantiated weapon
                ProjectileWeapon projectileWeapon = weaponInstance.GetComponent<ProjectileWeapon>();

                if (projectileWeapon != null)
                {
                    // Set the weapon's ammo
                    projectileWeapon.SetAmmo(weaponAmmo);

                    // Add the weapon to the weapons dictionary
                    weapons.Add(weaponData, projectileWeapon);
                }
            }
        }
    }
}

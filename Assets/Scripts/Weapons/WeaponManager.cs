using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponHolder; // The parent Transform where weapon instances will be created

    private Dictionary<string, ProjectileWeapon> weapons = new Dictionary<string, ProjectileWeapon>();

    public void SetupWeapons(Dictionary<string, int> weaponsAndAmmo)
    {
        foreach (KeyValuePair<string, int> weaponAndAmmo in weaponsAndAmmo)
        {
            // Get the weapon name and ammo
            string weaponName = weaponAndAmmo.Key;
            int weaponAmmo = weaponAndAmmo.Value;

            // Load the WeaponData from the Resources folder
            WeaponData weaponData = Resources.Load<WeaponData>($"Weapons/{weaponName}");

            if (weaponData != null)
            {
                // Instantiate the weapon and set up its visuals
                GameObject weaponInstance = new GameObject(weaponName + " Sprite");
                weaponInstance.AddComponent<SpriteRenderer>();
                weaponInstance.GetComponent<SpriteRenderer>().sprite = weaponData.weaponVisualsData.mainMenuSprite;

                // Set the weapon instance parent to the weaponHolder
                weaponInstance.transform.SetParent(weaponHolder);

                // Get the ProjectileWeapon component from the instantiated weapon
                ProjectileWeapon projectileWeapon = weaponInstance.GetComponent<ProjectileWeapon>();

                if (projectileWeapon != null)
                {
                    // Set the weapon's ammo
                    projectileWeapon.SetAmmo(weaponAmmo);

                    // Add the weapon to the weapons dictionary
                    weapons.Add(weaponName, projectileWeapon);
                }
            }
        }
    }
}
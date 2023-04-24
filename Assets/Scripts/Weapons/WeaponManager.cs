using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponHolder; // The parent Transform where weapon instances will be created

    private Dictionary<string, ProjectileWeapon> weapons = new Dictionary<string, ProjectileWeapon>();
    public ProjectileWeapon CurrentWeapon { get; set; }
    public void SetCurrentWeapon(string weaponName)
    {
        if (weapons.ContainsKey(weaponName))
        {
            CurrentWeapon = weapons[weaponName];
        }
    }

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

                GameObject weaponPrefab = weaponData.weaponPrefab;
                GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder);


                // Set the weapon instance parent to the weaponHolder
                weaponInstance.transform.SetParent(weaponHolder);

                // Get the ProjectileWeapon component from the instantiated weapon
                ProjectileWeapon projectileWeapon = weaponInstance.GetComponent<ProjectileWeapon>();

                if (projectileWeapon != null)
                {
                    // Set the weapon's data
                    projectileWeapon.weaponData = weaponData;

                    // Set the weapon's ammo
                    projectileWeapon.SetAmmo(weaponAmmo);

                    // Add the weapon to the weapons dictionary
                    weapons.Add(weaponName, projectileWeapon);

                    Debug.Log($"Weapon {weaponName} added to the weapons dictionary.");

                }
            }
        }   // Set the first weapon in the dictionary as the current weapon (if any)
        if (weapons.Count > 0)
        {
            SetCurrentWeapon(weapons.Keys.First());
        }
    }
}
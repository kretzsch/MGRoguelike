using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponHolder; // The parent Transform where weapon instances will be created
    public void SetupWeapons()
    {
        foreach (KeyValuePair<string, int> weaponAndAmmo in LoadoutData.selectedWeaponsAndAmmo)
        {
            // Get the weapon name and ammo
            string weaponName = weaponAndAmmo.Key;
            int weaponAmmo = weaponAndAmmo.Value;

            Debug.Log(weaponName + weaponAmmo);

            // Find the weapon data in the weaponDataList
            // ... (the same code as before)

            // Instantiate the weapon and set up its visuals
            // ... (the same code as before)

            // Set the weapon's ammo and add it to the weapons dictionary
            // ... (the same code as before)
        }
    }
}

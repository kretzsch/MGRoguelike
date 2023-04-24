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
            string weaponName = weaponAndAmmo.Key;
            int weaponAmmo = weaponAndAmmo.Value;

            WeaponData weaponData = Resources.Load<WeaponData>($"WeaponData/{weaponName}");

            if (weaponData != null)
            {
                GameObject weaponPrefab = Resources.Load<GameObject>($"WeaponPrefabs/{weaponName}");

                if (weaponPrefab != null)
                {
                    GameObject weaponInstance = Instantiate(weaponPrefab, weaponHolder); // Instantiate the weapon prefab
                    ProjectileWeapon projectileWeapon = weaponInstance.GetComponent<ProjectileWeapon>(); // Get the ProjectileWeapon component from the instantiated weapon

                    if (projectileWeapon != null)
                    {
                        projectileWeapon.weaponData = weaponData;
                        if (weaponData.compatibleAmmo != null)
                        {
                            // Get the compatible ammo key from the weapon data
                            string ammoKey = weaponData.compatibleAmmo.ammoName;

                            // Check if the ammo key exists in the weaponsAndAmmo dictionary
                            if (weaponsAndAmmo.ContainsKey(ammoKey))
                            {
                                weaponAmmo = weaponsAndAmmo[ammoKey];
                            }
                        }
                        projectileWeapon.SetAmmo(weaponAmmo);

                        weapons.Add(weaponName, projectileWeapon);

                        // Set the CurrentWeapon if it's not set yet
                        if (CurrentWeapon == null)
                        {
                            SetCurrentWeapon(weaponName);
                        }
                    }
                }
                // Handle other weapon types, if needed
            }
        }
    }



}
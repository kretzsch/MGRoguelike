using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// this class is reuseable for every genre in this game. 
/// This class is responsible for managing the player's available weapons. 
/// It instantiates weapon prefabs and
/// sets up their corresponding ProjectileWeapon components based on the provided WeaponData. 
/// It also manages weapon switching, toggling the visibility of weapon sprites according to the selected weapon.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private Transform weaponHolder; // The parent Transform where weapon instances will be created

    private Dictionary<string, ProjectileWeapon> weapons = new Dictionary<string, ProjectileWeapon>();
    public ProjectileWeapon CurrentWeapon { get; set; }

    void Start()
    {
        // this is to make sure only one weapon is showing up on the start 
        // in a relative hacky way. 
        //SetCurrentWeapon(weapons.Keys.FirstOrDefault());
        //another hacky way to make sure all the ammo is shown the correct way in the ui
     //   CurrentWeapon.Reload(); 
    }

    public void SetCurrentWeapon(string weaponName)
    {
        if (weapons.ContainsKey(weaponName))
        {
            Debug.Log($"Switching to weapon: {weaponName}");
            // Hide all weapons
            foreach (var weapon in weapons.Values)
            {
                weapon.gameObject.SetActive(false);
            }

            // Show only the current weapon
            CurrentWeapon = weapons[weaponName];
            CurrentWeapon.gameObject.SetActive(true);
        }
        else { Debug.Log($"Weapon not found: {weaponName}"); }
    }


    public void SetupWeapons(Dictionary<string, int> weaponsAndAmmo, WeaponData.GameGenre currentGenre)
    {
        Debug.Log("Setting up weapons:");
        foreach (KeyValuePair<string, int> weaponAndAmmo in weaponsAndAmmo)
        {
            string weaponName = weaponAndAmmo.Key;
            int weaponAmmo = weaponAndAmmo.Value;

            WeaponData weaponData = Resources.Load<WeaponData>($"WeaponData/{weaponName}");

            if (weaponData != null)
            {
                GameObject weaponPrefab = weaponData.genrePrefabs.Find(gp => gp.genre == currentGenre)?.prefab;

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
                        Debug.Log($"Added weapon: {weaponName}"); // Log the weapon being added

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


    public void SwitchWeapon(int direction)
    {
        // Get the index of the current weapon in the weapons dictionary
        int currentIndex = weapons.Values.ToList().IndexOf(CurrentWeapon);
        Debug.Log($"Current weapon index: {currentIndex}");


        // Increment or decrement the index based on the direction value (wrap around if necessary)
        currentIndex = (currentIndex + direction + weapons.Count) % weapons.Count;
        Debug.Log($"New weapon index: {currentIndex}");

        // Set the new current weapon
        SetCurrentWeapon(weapons.Keys.ToList()[currentIndex]);
    }

}
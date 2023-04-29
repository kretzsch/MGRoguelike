using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// this class is reuseable for every genre. 
/// The gamecontroller gets the weapon and ammo data from the loadoutdata thats saved between scenes.
/// this in turn calls the weaponmanager.setupweapons to set up the weapons with the loaded data.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private WeaponData.GameGenre currentGenre;
    [SerializeField] private WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        Debug.Log($"WeaponManager reference: {weaponManager}");
    }

    private void Start()
    {
        // Get the weapons and ammo data from LoadoutData
        Dictionary<string, int> weaponsAndAmmo = LoadoutData.selectedWeaponsAndAmmo;

        // Debug the content of the dictionary
        Debug.Log("LoadoutData content: " + string.Join(", ", weaponsAndAmmo.Select(kv => kv.Key + ": " + kv.Value).ToArray()));

        // Pass the weapons and ammo data to the WeaponManager
        weaponManager.SetupWeapons(weaponsAndAmmo, currentGenre);
    }
}


using System.Collections.Generic;
using UnityEngine;

public class TopDownGameController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    private void Start()
    {
        // Get the weapons and ammo data from LoadoutData
        Dictionary<string, int> weaponsAndAmmo = LoadoutData.selectedWeaponsAndAmmo;

        // Pass the weapons and ammo data to the WeaponManager
        weaponManager.SetupWeapons(weaponsAndAmmo);
    }
}
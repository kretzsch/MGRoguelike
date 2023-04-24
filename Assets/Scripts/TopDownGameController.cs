using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1.	Players select weapons and ammo in the main menu, and the LoadoutManager stores the selected weapons and ammo.
/// 2.  The LoadoutManager.TransferDataToPlayerInventory() method is called, which transfers the selected weapons and ammo to the PlayerInventory.
/// 3.	When the top-down level is loaded, the TopDowngamecontroller script gets the PlayerInventory instance and gets the weapons and ammo.
/// 4.	The TopDowngamecontroller passes the weapons and ammo to the WeaponManager in the top-down scene.
/// 5.	The WeaponManager sets up the weapons and ammo for the top-down game based on the transferred data.
/// </summary>
public class TopDownGameController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    private void Start()
    {
        // Get the PlayerInventory instance
        PlayerInventory playerInventory = PlayerInventory.Instance;

        // Get the weapons and ammo data from PlayerInventory
        Dictionary<WeaponData, int> weaponsAndAmmo = playerInventory.GetInventory();

        // Pass the weapons and ammo data to the WeaponManager
        weaponManager.SetupWeapons(weaponsAndAmmo);
    }
}

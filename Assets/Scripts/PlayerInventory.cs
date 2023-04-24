using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 1.	Players select weapons and ammo in the main menu, and the LoadoutManager stores the selected weapons and ammo.
/// 2.  The LoadoutManager.TransferDataToPlayerInventory() method is called, which transfers the selected weapons and ammo to the PlayerInventory.
/// 3.	When the top-down level is loaded, the TopDowngamecontroller script gets the PlayerInventory instance and gets the weapons and ammo.
/// 4.	The TopDowngamecontroller passes the weapons and ammo to the WeaponManager in the top-down scene.
/// 5.	The WeaponManager sets up the weapons and ammo for the top-down game based on the transferred data.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private Dictionary<WeaponData, int> weaponsAndAmmo = new Dictionary<WeaponData, int>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetInventory(Dictionary<WeaponData, int> newInventory)
    {
        weaponsAndAmmo = newInventory;
    }

    public Dictionary<WeaponData, int> GetInventory()
    {
        return weaponsAndAmmo;
    }

    public WeaponData GetWeaponDataByName(string weaponName)
    {
        foreach (var weaponData in weaponsAndAmmo.Keys)
        {
            if (weaponData.weaponName == weaponName)
            {
                return weaponData;
            }
        }
        return null;
    }

    // Add other methods to manage the player's inventory, such as adding or removing items, as needed. 
    //Ammo being used up for example.
}

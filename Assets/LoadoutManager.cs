using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// LoadoutManager is responsible for managing the player's loadout, including
/// purchasing weapons and ammo, and keeping track of the player's budget.
/// </summary>
public class LoadoutManager : MonoBehaviour
{
    public int startingBudget = 1000;
    public List<WeaponData> availableWeapons;

    private int currentBudget;
    private Dictionary<string, int> selectedWeaponsAmmo;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentBudget = startingBudget;
        selectedWeaponsAmmo = new Dictionary<string, int>();
    }

    // Purchase a weapon if the player has enough budget
    public bool PurchaseWeapon(WeaponData weaponData)
    {
        if (currentBudget >= weaponData.cost)
        {
            currentBudget -= weaponData.cost;
            selectedWeaponsAmmo[weaponData.weaponName] = 0;
            return true;
        }
        return false;
    }

    // Purchase ammo for a weapon if the player has enough budget
    public bool PurchaseAmmo(WeaponData weaponData, int units)
    {
        int totalAmmoCost = weaponData.ammoData.ammoCostPerUnit * units;

        if (currentBudget >= totalAmmoCost)
        {
            currentBudget -= totalAmmoCost;
            selectedWeaponsAmmo[weaponData.weaponName] += units;
            return true;
        }
        return false;
    }

    // Get the current player budget
    public int GetCurrentBudget()
    {
        return currentBudget;
    }

    // Get the selected weapons and their ammo counts
    public Dictionary<string, int> GetSelectedWeaponsAmmo()
    {
        return selectedWeaponsAmmo;
    }
}

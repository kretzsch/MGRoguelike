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
    public List<AmmoData> availableAmmo;

    private int currentBudget;
    private Dictionary<string, int> selectedWeaponsAmmo;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentBudget = startingBudget;
        selectedWeaponsAmmo = new Dictionary<string, int>();
    }

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

    public bool PurchaseAmmo(AmmoData ammoData, int units)
    {
        int totalAmmoCost = ammoData.ammoCostPerUnit * units;

        if (currentBudget >= totalAmmoCost)
        {
            currentBudget -= totalAmmoCost;
            selectedWeaponsAmmo[ammoData.ammoName] += units;
            return true;
        }
        return false;
    }

    public int GetCurrentBudget()
    {
        return currentBudget;
    }

    public Dictionary<string, int> GetSelectedWeaponsAmmo()
    {
        return selectedWeaponsAmmo;
    }
}

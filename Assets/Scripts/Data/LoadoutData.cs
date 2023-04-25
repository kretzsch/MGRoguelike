using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// this is where the loadoutmanager from the main menu stores bought item data. 
/// this is done through the  TransferDataToLoadoutData() method.
/// </summary>
public static class LoadoutData
{
    public static Dictionary<string, int> selectedWeaponsAndAmmo = new Dictionary<string, int>();
    public static Dictionary<string, int> remainingAmmo = new Dictionary<string, int>();

    public static event Action<string, int, int> OnAmmoCountChanged;

    public static void UpdateAmmoCount(string ammoName, int newCurrentAmmo, int magazineSize)
    {
        if (selectedWeaponsAndAmmo.ContainsKey(ammoName))
        {
            selectedWeaponsAndAmmo[ammoName] = newCurrentAmmo;

            if (remainingAmmo.ContainsKey(ammoName))
            {
                int totalAmmo = selectedWeaponsAndAmmo[ammoName] + remainingAmmo[ammoName];
                int remaining = Mathf.Max(0, totalAmmo - newCurrentAmmo);
                remainingAmmo[ammoName] = remaining;
                OnAmmoCountChanged?.Invoke(ammoName, newCurrentAmmo, remaining);
            }
        }
    }


    public static void InitializeRemainingAmmo(string ammoName, int initialAmmo, int magazineSize)
    {
        if (!remainingAmmo.ContainsKey(ammoName))
        {
            remainingAmmo.Add(ammoName, Mathf.Max(0, initialAmmo - magazineSize));
            Debug.Log($"InitializeRemainingAmmo - AmmoName: {ammoName}, InitialAmmo: {initialAmmo}, RemainingAmmo: {remainingAmmo[ammoName]}");

        }
    }
}



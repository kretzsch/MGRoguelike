using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopDownGameController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    private void Start()
    {
        // Get the weapons and ammo data from LoadoutData
        Dictionary<string, int> weaponsAndAmmo = LoadoutData.selectedWeaponsAndAmmo;

        // Debug the content of the dictionary
        Debug.Log("LoadoutData content: " + string.Join(", ", weaponsAndAmmo.Select(kv => kv.Key + ": " + kv.Value).ToArray()));

        // Pass the weapons and ammo data to the WeaponManager
        weaponManager.SetupWeapons(weaponsAndAmmo);
    }
}

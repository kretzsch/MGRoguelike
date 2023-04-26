using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopDownGameController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    public WeaponData.GameGenre currentGenre = WeaponData.GameGenre.TopDown; // we can hardcode this since we're already in the topdowncontroller.

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

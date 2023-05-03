using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This class is responsible for setting up and managing the in-game user interface elements, such as
///  the weapon icons and ammo counters. 
///  It reads the selected weapons and ammo from LoadoutData and creates UI elements accordingly. 
///  The class also listens for ammo count changes in LoadoutData and
///  updates the corresponding ammo count text in the UI.
/// </summary>
public class InGameUISetup : MonoBehaviour
{
    [SerializeField] private Transform weaponItemsParent;

    private Dictionary<string, TextMeshProUGUI> ammoTextObjects = new Dictionary<string, TextMeshProUGUI>();

    private void Start()
    {
        SetupInGameUI();
        LoadoutData.OnAmmoCountChanged += UpdateAmmoCountText;
    }

    private void OnDestroy()
    {
        LoadoutData.OnAmmoCountChanged -= UpdateAmmoCountText;
    }

    private void SetupInGameUI()
    {
        foreach (KeyValuePair<string, int> weaponAndAmmo in LoadoutData.selectedWeaponsAndAmmo)
        {
            WeaponData weaponData = Resources.Load<WeaponData>($"WeaponData/{weaponAndAmmo.Key}");

            if (weaponData != null && weaponData.compatibleAmmo != null)
            {
                string ammoKey = weaponData.compatibleAmmo.ammoName;

                if (LoadoutData.selectedWeaponsAndAmmo.ContainsKey(ammoKey))
                {
                    int ammoCount = LoadoutData.selectedWeaponsAndAmmo[ammoKey];
                    AddWeaponItemToUI(weaponData, ammoCount, weaponItemsParent);

                    // Get the initial ammo in the magazine
                    int initialAmmoInMagazine = weaponData.initialAmmoInMagazine;

                    // Initialize remaining ammo
                    LoadoutData.InitializeRemainingAmmo(ammoKey, ammoCount, initialAmmoInMagazine);
                }
            }
        }
    }



    private void AddWeaponItemToUI(WeaponData weaponData, int ammoCount, Transform weaponItemsParent)
    {
        GameObject weaponItem = new GameObject(weaponData.weaponName + " Item");
        Image weaponImage = weaponItem.AddComponent<Image>();
        weaponImage.sprite = weaponData.weaponVisualsData.mainMenuSprite;
        weaponImage.color = Color.white;
        weaponImage.preserveAspect = true;
        weaponItem.transform.SetParent(weaponItemsParent, false);

        if (weaponData.compatibleAmmo != null)
        {
            GameObject ammoCountTextObject = new GameObject("Ammo Count Text");
            TextMeshProUGUI ammoCountText = ammoCountTextObject.AddComponent<TextMeshProUGUI>();
            ammoCountText.text = ammoCount.ToString();
            ammoCountText.alignment = TextAlignmentOptions.Center;
            ammoCountText.color = Color.white;
            ammoCountTextObject.transform.SetParent(weaponItem.transform, false);

            // Add ammo count TextMeshProUGUI to the dictionary
            ammoTextObjects.Add(weaponData.compatibleAmmo.ammoName, ammoCountText);
        }
    }

    private void UpdateAmmoCountText(string ammoName, int newCurrentAmmo, int newRemainingAmmo)
    {
        if (ammoTextObjects.ContainsKey(ammoName))
        {
            ammoTextObjects[ammoName].text = $"{newCurrentAmmo}/{newRemainingAmmo}";
            Debug.Log($"UpdateAmmoCountText - AmmoName: {ammoName}, NewCurrentAmmo: {newCurrentAmmo}, NewRemainingAmmo: {newRemainingAmmo}");

        }
    }

}

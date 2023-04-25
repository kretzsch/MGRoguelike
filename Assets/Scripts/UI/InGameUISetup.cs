using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

                    // Initialize remaining ammo
                    LoadoutData.InitializeRemainingAmmo(ammoKey, ammoCount, weaponData.magazineSize);

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

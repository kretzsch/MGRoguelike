using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopDownGameController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private Transform weaponItemsParent;
    //[SerializeField] private Transform ammoParent;

    private void Start()
    {
        // Get the weapons and ammo data from LoadoutData
        Dictionary<string, int> weaponsAndAmmo = LoadoutData.selectedWeaponsAndAmmo;

        // Pass the weapons and ammo data to the WeaponManager
        weaponManager.SetupWeapons(weaponsAndAmmo);

        // Set up the in-game UI
        SetupInGameUI(weaponItemsParent);
    }

    public void SetupInGameUI(Transform weaponItemsParent)
    {
        // Iterate through the selected weapons and ammo data
        foreach (KeyValuePair<string, int> weaponAndAmmo in LoadoutData.selectedWeaponsAndAmmo)
        {
            // Load the WeaponData from the Resources folder
            WeaponData weaponData = Resources.Load<WeaponData>($"Weapons/{weaponAndAmmo.Key}");

            if (weaponData != null)
            {
                int ammoCount = 0;

                if (weaponData.compatibleAmmo != null)
                {
                    // Get the compatible ammo key from the weapon data
                    string ammoKey = weaponData.compatibleAmmo.ammoName;

                    // Check if the ammo key exists in the selectedWeaponsAndAmmo dictionary
                    if (LoadoutData.selectedWeaponsAndAmmo.ContainsKey(ammoKey))
                    {
                        ammoCount = LoadoutData.selectedWeaponsAndAmmo[ammoKey];
                    }
                }

                // Add the weapon item to the UI with the correct ammo count
                AddWeaponItemToUI(weaponData, ammoCount, weaponItemsParent);
            }
        }
    }



    public void AddWeaponItemToUI(WeaponData weaponData, int ammoCount, Transform weaponItemsParent)
    {
        // Create a new GameObject with an Image component
        GameObject weaponItem = new GameObject(weaponData.weaponName + " Item");
        Image weaponImage = weaponItem.AddComponent<Image>();

        // Assign the main menu sprite to the Image component
        weaponImage.sprite = weaponData.weaponVisualsData.mainMenuSprite;
        weaponImage.color = Color.white;
        weaponImage.preserveAspect = true;

        // Set the new GameObject as a child of weaponItemsParent
        weaponItem.transform.SetParent(weaponItemsParent, false);

        if (weaponData.compatibleAmmo != null)
        {
            // Add a TextMeshProUGUI component to display the ammo count
            GameObject ammoCountTextObject = new GameObject("Ammo Count Text");
            TextMeshProUGUI ammoCountText = ammoCountTextObject.AddComponent<TextMeshProUGUI>();
            ammoCountText.text = ammoCount.ToString();
            ammoCountText.alignment = TextAlignmentOptions.Center;
            ammoCountText.color = Color.white;

            // Set the TextMeshProUGUI object as a child of the weapon item
            ammoCountTextObject.transform.SetParent(weaponItem.transform, false);
        }
    }
}

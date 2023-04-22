using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Purchase a weapon and update the budget and UI accordingly
    public bool PurchaseWeapon(WeaponData weaponData, TextMeshProUGUI budgetText, Transform purchasedItemsParent)
    {
        if (currentBudget >= weaponData.cost)
        {
            currentBudget -= weaponData.cost;
            selectedWeaponsAmmo[weaponData.weaponName] = 0;
            UpdateBudgetUI(budgetText);
            AddWeaponToUI(weaponData, purchasedItemsParent);
            return true;
        }
        return false;
    }

    // Purchase ammo and update the budget and UI accordingly
    public bool PurchaseAmmo(AmmoData ammoData, int units, TextMeshProUGUI budgetText, Transform purchasedItemsParent)
    {
        int totalAmmoCost = ammoData.ammoCostPerUnit * units;

        if (currentBudget >= totalAmmoCost)
        {
            currentBudget -= totalAmmoCost;
            selectedWeaponsAmmo[ammoData.ammoName] += units;
            UpdateBudgetUI(budgetText);
            AddAmmoToUI(ammoData, purchasedItemsParent);
            return true;
        }
        return false;
    }

    // Update the UI to show the current budget
    public void UpdateBudgetUI(TextMeshProUGUI budgetText)
    {
        budgetText.text = $"Budget: ${currentBudget}";
    }

    // Add a weapon icon to the UI
    public void AddWeaponToUI(WeaponData weaponData, Transform purchasedItemsParent)
    {
        Instantiate(weaponData.weaponVisualsData.levelVisuals[0].sprite, purchasedItemsParent);
    }

    // Add an ammo icon and count to the UI
    public void AddAmmoToUI(AmmoData ammoData, Transform purchasedItemsParent)
    {
        // Instantiate the ammo icon prefab
      //  GameObject ammoIcon = Instantiate(ammoData.visuals.ammoIconPrefab, purchasedItemsParent);

        // Update the ammo count text
       // TextMeshProUGUI ammoCountText = ammoIcon.GetComponentInChildren<TextMeshProUGUI>();
       // ammoCountText.text = selectedWeaponsAmmo[ammoData.ammoName].ToString();
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

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// LoadoutManager handles the player's weapon and ammo purchases, as well as the UI updates for budget and purchased items.
/// </summary>
public class LoadoutManager : MonoBehaviour
{
    public int startingBudget = 1000;
    public List<WeaponData> availableWeapons;
    public List<AmmoData> availableAmmo;

    private int currentBudget;
    private Dictionary<string, int> selectedWeaponsAndAmmo;
    public TextMeshProUGUI budgetText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        currentBudget = startingBudget;
        selectedWeaponsAndAmmo = new Dictionary<string, int>();
        UpdateBudgetUI(budgetText);
    }

    public bool PurchaseWeapon(WeaponData weaponData, TextMeshProUGUI budgetText, Transform purchasedItemsParent)
    {
        // Check if the weapon is already purchased
        if (selectedWeaponsAndAmmo.ContainsKey(weaponData.weaponName))
        {
            Debug.Log("Weapon already purchased.");
            return false;
        }

        if (currentBudget >= weaponData.cost)
        {
            currentBudget -= weaponData.cost;
            selectedWeaponsAndAmmo[weaponData.weaponName] = 0;
            UpdateBudgetUI(budgetText);
            AddWeaponToUI(weaponData, purchasedItemsParent);
            return true;
        }
        return false;
    }


    public bool PurchaseAmmo(AmmoData ammoData, int units, TextMeshProUGUI budgetText, Transform purchasedItemsParent)
    {
        int totalAmmoCost = ammoData.Cost * units;

        if (currentBudget >= totalAmmoCost)
        {
            currentBudget -= totalAmmoCost;

            // Check if the key exists in the dictionary, if not, add it with an initial value of 0
            if (!selectedWeaponsAndAmmo.ContainsKey(ammoData.ItemName))
            {
                selectedWeaponsAndAmmo[ammoData.ItemName] = 0;
            }
            selectedWeaponsAndAmmo[ammoData.ItemName] += units;

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

    public void AddWeaponToUI(WeaponData weaponData, Transform purchasedItemsParent)
    {
        // Determine the number of columns for the grid
        int numberOfColumns = 4;

        // Calculate the cell size based on the parent panel size and number of columns
        RectTransform parentRectTransform = purchasedItemsParent.GetComponent<RectTransform>();
        float cellWidth = parentRectTransform.rect.width / numberOfColumns;
        float cellHeight = parentRectTransform.rect.height / numberOfColumns;

        // Set the cell size in the GridLayoutGroup component
        GridLayoutGroup gridLayoutGroup = purchasedItemsParent.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);

        // Create a new GameObject with an Image component
        GameObject weaponIcon = new GameObject(weaponData.weaponName + " Icon");
        Image weaponImage = weaponIcon.AddComponent<Image>();

        // Assign the main menu sprite to the Image component
        weaponImage.sprite = weaponData.weaponVisualsData.mainMenuSprite;
        weaponImage.color = Color.white;
        weaponImage.preserveAspect = true;

        // Set the new GameObject as a child of purchasedItemsParent
        weaponIcon.transform.SetParent(purchasedItemsParent, false);

        // Get the RectTransform component from the weapon icon
        RectTransform rectTransform = weaponIcon.GetComponent<RectTransform>();

        // Reset the RectTransform values to fit the GridLayoutGroup cell size
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(cellWidth, cellHeight);
    }



    public void AddAmmoToUI(AmmoData ammoData, Transform purchasedItemsParent)
    {
        // Check if the ammo icon already exists in the UI
        Transform existingAmmoIcon = purchasedItemsParent.Find(ammoData.ammoName + " Icon");
        if (existingAmmoIcon != null)
        {
            // Update the ammo count text
            TextMeshProUGUI ammoText = existingAmmoIcon.GetComponentInChildren<TextMeshProUGUI>();
            ammoText.color = Color.white;
            ammoText.text = selectedWeaponsAndAmmo[ammoData.ammoName].ToString();
            return;
        }
        // Force the layout to recalculate before adding the ammo icon
        // this is done so that a bug doesnt occur where the ammo icon takes up the whole panel
        LayoutRebuilder.ForceRebuildLayoutImmediate(purchasedItemsParent.GetComponent<RectTransform>());

        // Create a new GameObject with an Image component
        GameObject ammoIcon = new GameObject(ammoData.ammoName + " Icon");
        Image ammoImage = ammoIcon.AddComponent<Image>();

        // Assign the ammo icon sprite to the Image component
        ammoImage.sprite = ammoData.mainMenuSprite;
        ammoImage.color = Color.white;
        ammoImage.preserveAspect = true;

        // Set the new GameObject as a child of purchasedItemsParent
        ammoIcon.transform.SetParent(purchasedItemsParent, false);

        // Get the RectTransform component from the ammo icon
        RectTransform rectTransform = ammoIcon.GetComponent<RectTransform>();

        // Reset the RectTransform values to fit the GridLayoutGroup cell size
        GridLayoutGroup gridLayoutGroup = purchasedItemsParent.GetComponent<GridLayoutGroup>();
        Vector2 cellSize = gridLayoutGroup.cellSize;
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.sizeDelta = new Vector2(cellSize.x, cellSize.y);

        // Add a TextMeshProUGUI component to display the ammo count
        GameObject ammoCountTextObject = new GameObject("Ammo Count Text");
        TextMeshProUGUI ammoCountText = ammoCountTextObject.AddComponent<TextMeshProUGUI>();
        ammoCountText.text = selectedWeaponsAndAmmo[ammoData.ammoName].ToString();
        ammoCountText.alignment = TextAlignmentOptions.Center;
        ammoCountText.color = Color.white;

        // Set the TextMeshProUGUI object as a child of the ammo icon
        ammoCountTextObject.transform.SetParent(ammoIcon.transform, false);
    }


    public int GetCurrentBudget()
    {
        return currentBudget;
    }

    public Dictionary<string, int> GetSelectedWeaponsAmmo()
    {
        return selectedWeaponsAndAmmo;
    }
    public void ResetLoadout(TextMeshProUGUI budgetText, Transform purchasedItemsParent)
    {
        currentBudget = startingBudget;
        selectedWeaponsAndAmmo.Clear();
        UpdateBudgetUI(budgetText);
        ClearPurchasedItemsUI(purchasedItemsParent);
    }

    private void ClearPurchasedItemsUI(Transform purchasedItemsParent)
    {
        foreach (Transform child in purchasedItemsParent)
        {
            Destroy(child.gameObject);
        }
    }

}

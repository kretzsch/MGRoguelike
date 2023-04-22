using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PurchaseButton is a script that handles the purchasing of weapons when a button is clicked.
/// It listens for a button click and calls the PurchaseWeapon method on the LoadoutManager.
/// </summary>
public class PurchaseButton : MonoBehaviour
{
    public IPurchaseable purchaseableItem;
    public TextMeshProUGUI budgetText;
    public Transform purchasedItemsParent;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PurchaseItem);
    }

    private void PurchaseItem()
    {
        LoadoutManager loadoutManager = FindObjectOfType<LoadoutManager>();
        if (loadoutManager != null)
        {
            if (purchaseableItem is WeaponData weaponData)
            {
                loadoutManager.PurchaseWeapon(weaponData, budgetText, purchasedItemsParent);
            }
            else if (purchaseableItem is AmmoData ammoData)
            {
                int units = 1;
                loadoutManager.PurchaseAmmo(ammoData, units, budgetText, purchasedItemsParent);
            }
            // Add more conditions here for other purchaseable items like armor or grenades
        }
    }
}


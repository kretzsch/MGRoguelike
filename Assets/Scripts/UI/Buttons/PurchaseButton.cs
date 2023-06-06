using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PurchaseButton is a script that handles the purchasing of items when a button is clicked.
/// It listens for a button click and calls the appropriate purchase method on the LoadoutManager.
/// </summary>
public class PurchaseButton : MonoBehaviour
{
    public PurchaseableItem purchaseableItem;
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
                // Add the necessary arguments like 'units' for ammo purchase
                int units = ammoData.magazineSize;
                loadoutManager.PurchaseAmmo(ammoData, units, budgetText, purchasedItemsParent);

            }
            // Add more conditions here for other purchasable items like armor or grenades
        }
    }
}

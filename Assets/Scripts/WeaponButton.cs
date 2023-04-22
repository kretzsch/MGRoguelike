using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// WeaponButton is a script that handles the purchasing of weapons when a button is clicked.
/// It listens for a button click and calls the PurchaseWeapon method on the LoadoutManager.
/// </summary>
public class WeaponButton : MonoBehaviour
{
    public WeaponData weaponData;
    public TextMeshProUGUI budgetText;
    public Transform purchasedItemsParent;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PurchaseWeapon);
    }

    private void PurchaseWeapon()
    {
        LoadoutManager loadoutManager = FindObjectOfType<LoadoutManager>();
        if (loadoutManager != null)
        {
            loadoutManager.PurchaseWeapon(weaponData, budgetText, purchasedItemsParent);
        }
    }
}

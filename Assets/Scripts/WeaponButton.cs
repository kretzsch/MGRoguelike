using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
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

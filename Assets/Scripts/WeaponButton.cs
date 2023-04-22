using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    public WeaponData weaponData;

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
            loadoutManager.PurchaseWeapon(weaponData);
        }
    }
}

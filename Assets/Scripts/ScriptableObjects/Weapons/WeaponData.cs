using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject, IPurchaseable
{
    public string weaponName;
    public int cost;
    public int magazineSize;
    public WeaponVisualsData weaponVisualsData;
    public AmmoData compatibleAmmo;

    // Implementing IPurchaseable interface using properties
    public string ItemName => weaponName;
    public int Cost => cost;
}



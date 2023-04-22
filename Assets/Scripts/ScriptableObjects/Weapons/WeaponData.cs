using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : PurchaseableItem
{
    public string weaponName;
    public int cost;
    public int magazineSize;
    public WeaponVisualsData weaponVisualsData;
    public AmmoData compatibleAmmo;

    public override string ItemName => weaponName;
    public override int Cost => cost;
}

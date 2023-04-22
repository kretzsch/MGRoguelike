using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int cost;
    public int magazineSize;
    public WeaponVisualsData weaponVisualsData;
    public AmmoData compatibleAmmo;
}

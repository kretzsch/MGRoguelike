using UnityEngine;
/// <summary>
/// This class is a ScriptableObject that stores data about a weapon's properties, such as
/// its name, damage, fire rate, and compatible ammo. 
/// It is used by the ProjectileWeapon class to set up the weapon instance according to the defined data.
/// </summary>
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]
public class WeaponData : PurchaseableItem
{
    public string weaponName;
    public int cost;
    public int magazineSize; 
    public int damage;

    public WeaponVisualsData weaponVisualsData;
    public AmmoData compatibleAmmo;
    public GameObject weaponPrefab; // currently this is the topdown prefab only

    public enum GameGenre
    {
        Platformer,
        TopDown,
        // Add other genres here
    }

    public override string ItemName => weaponName;
    public override int Cost => cost;
}

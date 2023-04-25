using UnityEngine;

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

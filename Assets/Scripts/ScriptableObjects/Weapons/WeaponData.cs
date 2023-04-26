using System.Collections.Generic;
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

    [Tooltip("Genre-specific prefabs for this weapon")]
    public List<GenrePrefab> genrePrefabs;

    public enum GameGenre
    {
        Platformer,
        TopDown,
        FPS,
        Invaders
        // Add other genres here
    }

    public override string ItemName => weaponName;
    public override int Cost => cost;

    [System.Serializable]
    public class GenrePrefab
    {
        public GameGenre genre;
        public GameObject prefab;
    }
}

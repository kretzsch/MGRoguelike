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

    //used to initialize the UI ammo shown
    public int initialAmmoInMagazine;

    public AudioEvent weaponAudioEvent; 


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
    [System.Serializable]
    public class AudioEvent
    {
        public string EventPath2D;
        public string EventPath3D;
    }

}

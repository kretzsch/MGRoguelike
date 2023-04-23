using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The WeaponVisuals ScriptableObject is created to separate the weapon's visual
/// representation from its functionality. This allows for easy management of different
/// sprites or 3D models for each level or genre without affecting the core weapon functionality.
/// </summary>
[CreateAssetMenu(fileName = "NewWeaponVisuals", menuName = "Weapon/WeaponVisuals")]
public class WeaponVisualsData : ScriptableObject
{
    // A list of visual representations for the weapon in different levels or genres.
    public List<LevelVisual> levelVisuals;
    public Sprite mainMenuSprite;

    //since you cant store a transform or gameobject into a scriptable object i went for a string 
    // to prevent unnecessary complexion
    public string mainMenuModelIdentifier;

    // The LevelVisual class holds the visual data for a weapon in a specific level or genre.
    [System.Serializable]
    public class LevelVisual
    {
        public string levelName;
        public Sprite sprite;
        public GameObject modelPrefab;
    }
}

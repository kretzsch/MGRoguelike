using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "Weapon/AmmoData", order = 2)]
public class AmmoData : ScriptableObject, IPurchaseable
{
    public string ammoName;
    public int ammoCostPerUnit;
    public Sprite mainMenuSprite;

    // Implementing IPurchaseable interface using properties
    public string ItemName => ammoName;
    public int Cost => ammoCostPerUnit;
}

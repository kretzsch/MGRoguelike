using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "Weapon/AmmoData", order = 2)]
public class AmmoData : PurchaseableItem
{
    public string ammoName;
    public int ammoCostPerUnit;
    public int magazineSize;
    public Sprite mainMenuSprite;
   

    public override string ItemName => ammoName;
    public override int Cost => ammoCostPerUnit;
}

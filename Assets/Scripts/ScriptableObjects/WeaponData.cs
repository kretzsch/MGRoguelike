using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int cost;
    public AmmoData ammoData;
    public int magazineSize;
}

using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "Weapon/AmmoData", order = 2)]
public class AmmoData : ScriptableObject
{
    public string ammoName;
    public int ammoCostPerUnit;
}

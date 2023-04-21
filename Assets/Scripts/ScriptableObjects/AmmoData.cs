using UnityEngine;

[CreateAssetMenu(fileName = "AmmoData", menuName = "ScriptableObjects/AmmoData", order = 2)]
public class AmmoData : ScriptableObject
{
    public string ammoName;
    public int ammoCostPerUnit;
}

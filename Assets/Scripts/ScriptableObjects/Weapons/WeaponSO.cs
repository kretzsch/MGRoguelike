using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon")]
public class WeaponSO : ScriptableObject
{
  public GameObject bulletType;
  public float fireForce; 
  public Sprite artwork;
  public Vector2 bulletSpawnPos;
}

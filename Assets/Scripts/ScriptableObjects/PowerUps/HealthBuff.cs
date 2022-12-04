using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PowerUp", menuName = "ScriptableObjects/PowerUp/Health")]
public class HealthBuff : PowerUpSO
{
  public int amount;
  [SerializeField] private int buffTime;
  //get
  public override int BuffTime => buffTime;

  public override void Apply(GameObject target)
  {
    target.GetComponent<PlayerStats>().currentHealth += amount;
    Debug.Log($"{amount}");
  }
  //after X wait seconds do buff
  public override void Remove(GameObject target)
  {
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PowerUp", menuName = "ScriptableObjects/PowerUp/Speed")]
public class SpeedBuff : PowerUpSO
{
  public float amount;
  [SerializeField] private int buffTime;

  //get
  public override int BuffTime => buffTime;

  //Apply the buff
  public override void Apply(GameObject target)
  {
    target.GetComponent<PlayerController>().speedBuff += amount;
  }


  //after wait X seconds do remove buff
  public override void Remove(GameObject target)
  {
    target.GetComponent<PlayerController>().speedBuff -= amount;
  }
}

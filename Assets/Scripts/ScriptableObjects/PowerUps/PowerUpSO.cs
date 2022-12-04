using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpSO : ScriptableObject
{
  public abstract int BuffTime
  {
    get;
  }
  public abstract void Apply(GameObject target);
  public abstract void Remove(GameObject target);
}

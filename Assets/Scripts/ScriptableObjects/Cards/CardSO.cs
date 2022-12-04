using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Card", menuName = "ScriptableObjects/Card")]
public class CardSO : ScriptableObject
{
// [Header]
  public new string name;
  public CardContainer.CardType cardType;
  public CardContainer.BulletType bulletType;
  public string description;
  public Sprite artwork;
  public int manaCost;
  
}

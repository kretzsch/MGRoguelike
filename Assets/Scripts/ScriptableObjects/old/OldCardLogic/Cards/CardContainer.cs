using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

public class CardContainer : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
  [HideInInspector] public bool _hasBeenPlayed;
  [HideInInspector] public int _handIndex;
  private CardManager _cardManager;



  [Header("ScriptableOBJ variables")]
  public CardSO card; //insert scriptable object Card 
  public Image artworkImage;
  [Header("Text Objects")]
  public TMP_Text nameText;
  public TMP_Text descriptionText;
  public TMP_Text manaText;

  [Header("Weapon-Type Only")]
  public GameObject weaponPrefab;
  private GameObject[] weapons;


  [Header("Ammo-Type Only")]
  public int ammoAmount;

  [Header("Shield-Type Only")]
  public int shieldAmount;


  public enum CardType
  {
    Weapon, //contains ammo
    Effect,
    Shield,
    Ammo
  }

  public enum BulletType
  {
    FastBullet,
    SlowBullet
  }
  private CardType _cardType;
  private BulletType _bulletType;

  private void Start()
  {
    #region scriptableobj start
    nameText.text = card.name;
    manaText.text = $"{card.manaCost}";
    descriptionText.text = card.description;
    _cardType = card.cardType;
    _bulletType = card.bulletType;
    #endregion
    _cardManager = FindObjectOfType<CardManager>();

    weapons = GameObject.FindGameObjectsWithTag("Weapon");
    foreach (GameObject weapon in weapons)
    {
      Debug.Log($"{weapon.name}");
    }
  }


  #region IHandlers (click and drag logic)

  public void OnDrag(PointerEventData eventData)
  {
    transform.position = eventData.position;
  }


  //when released from drag do card effects
  public void OnPointerUp(PointerEventData eventData)
  {
    if (!_hasBeenPlayed)
    {
      //checks card type and runs methods accordingly
      switch (_cardType)
      {
        case CardType.Weapon:
          Debug.Log("I'm weapon number " + $"{card.manaCost}");
          //Sets all children inactive
          // weaponManager.DisableWeapon();
          SpawnWeapon();
          break;
        case CardType.Ammo:
          AddBullet();
          break;
        case CardType.Shield:
          break;
        case CardType.Effect:
          break;
        default:
          break;
      }
      _hasBeenPlayed = true;
      _cardManager.availableHandSlots[_handIndex] = true; //Sets the availabilty of the slot to true so it can get filled again
      Invoke("MoveToDiscardPile", 2f);

    }
  }
  //OnPointerDown is also required to receive OnPointerUp callbacks
  public void OnPointerDown(PointerEventData eventData)
  {
  }
  #endregion
  private void MoveToDiscardPile()
  {
    _cardManager.discardPile.Add(this);
    gameObject.SetActive(false);
  }

  #region Card Type effect Methods
  void SpawnWeapon()
  {
    foreach (Transform weapon in weaponPrefab.transform.parent)
    {
      weapon.gameObject.SetActive(false);
    }
    weaponPrefab.SetActive(true);
  }
  void AddBullet()
  {
    //ADD LOGIC OF ADDING BULLET TO RESOURCEMNGR.
    switch (_bulletType)
    {
      case BulletType.FastBullet:

        //Fastbullet ammo ++
        break;
      case BulletType.SlowBullet:
        //slowBullet ammo++
        break;
      default:
        break;
    }

  }
  #endregion
}

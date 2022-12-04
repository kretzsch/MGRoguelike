using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
  [Header("Deck")]
  public CardSO[] cardSOArray;
  public List<CardContainer> deck;
  public TMP_Text decksizeTxt;

  [Header("Discard")]
  public List<CardContainer> discardPile;
  public TMP_Text discardsizeTxt;

  [Header("Hand")]
  public Transform[] handSlots;
  public bool[] availableHandSlots;
  public List<CardContainer> hand;
  private int _maxHandSize = 5;
  public CardContainer currentCard;

  
  private void Start()
  {
    // FillDeck();
    ShuffleDeck();
  }

  #region todo drawcard and handcontroller logic
  /*
  public void DrawCards(int numCards)
  {
    for (int i = 0; i < numCards; i++)
    {
      if (hand.Count < _maxHandSize)
      {
        if (deck.Count != 0)
        {
          hand.Add(deck[0]); //order matters!
          deck.RemoveAt(0);
          handController.AddCardToHand(hand[hand.Count - 1], hand.Count);
        }
      }
    }
  }
  public void SetCurrentCard (int handIndex)
  {
    currentCard = hand[handIndex];
    currentAction = currentCard.name;
  }

  public void DiscardCurrentHand()
  {
    for (int i = 0; i < hand.Count; i++)
    {
      AddCardToDiscard(hand[i], true);
    }
    hand.Clear();
    handController.ClearHand();
  } */
  #endregion

  #region tempDrawCard And handcontroller logic
  public void DrawCard()
  {
    for (int i = 0; i < availableHandSlots.Length; i++)
    {
      if (availableHandSlots[i] == true)
      {
        if (deck.Count >= 1)
        {
          //switch to pooling?
          //takes the bottom card of the deck and sets it to active and puts it in the hand
          deck[0].gameObject.SetActive(true);
          deck[0]._handIndex = i;
          deck[0].transform.position = handSlots[i].position;
          deck[0]._hasBeenPlayed = false;

          availableHandSlots[i] = false;
          deck.Remove(deck[0]);
        }
      }
    }
  }
  #endregion

  private void Update()
  {
    //does this really need to be in the update?
    decksizeTxt.text = $"{deck.Count}";
    discardsizeTxt.text = $"{discardPile.Count}";
  }

  private void ShuffleDeck()
  {
    if (deck.Count > 1) deck = deck.OrderBy(x => Random.value).ToList();
    #region Modern FY shuffle
    /*int n = deck.Count; //modern FY shuffle
    while (n > 1)
    {
      n--;
      int random = Random.Range(0, n - 1);
      //swap
      CardSO temp = deck[n];
      deck[n] = deck[random];
      deck[random] = temp;
    } */
    #endregion
  }


  /// <summary>
  ///  populates the deck from the discard pile 
  /// </summary>
  public void PopulateDeck()
  {
    if (discardPile.Count >= 1)
    {
      foreach (CardContainer cardContainer in discardPile)
      {
        deck.Add(cardContainer);
      }
      discardPile.Clear();
      ShuffleDeck();
    }
  }
}


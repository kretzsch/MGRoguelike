using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
public class PowerUpContainer : MonoBehaviour
{
  public PowerUpSO powerUpSO;
  public GameObject pickupEffect;
  private int _buffTime;

  void Start()
  {
    _buffTime = powerUpSO.BuffTime;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      StartCoroutine(Pickup(collision));
    }
  }
  IEnumerator Pickup(Collider2D player)
  {
    powerUpSO.Apply(player.gameObject);
    Debug.Log($"{_buffTime}");
    GetComponent<Collider2D>().enabled = false;
    GetComponent<SpriteRenderer>().enabled = false;
    yield return new WaitForSeconds(_buffTime);

    powerUpSO.Remove(player.gameObject);
    Destroy(gameObject);
  }
}

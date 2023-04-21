using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  public Rigidbody2D thisRB;

  private void OnTriggerEnter2D(Collider2D other)
  {
    //TODO: replace with object pooling
    //make bullets dissapear when colliding, let the enemy take damage
    switch (other.gameObject.tag)
    {
      case "Wall":
        Destroy(gameObject);
        break;
      case "Enemy":
        //other.gameObject.GetComponent<Enemy>().TakeDamage();
        Destroy(gameObject);
        break;
      default:
        break;
    }
  }
}

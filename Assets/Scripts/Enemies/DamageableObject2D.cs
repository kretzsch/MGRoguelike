using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject2D : DamageableObject
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    public override void HandleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            player.TakeDamage(5); // example value
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject3D : DamageableObject
{
    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    public override void HandleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            player.TakeDamage(10); // example value
        }
    }
}


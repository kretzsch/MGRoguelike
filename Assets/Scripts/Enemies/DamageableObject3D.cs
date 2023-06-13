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
            Debug.Log("3D COLLISION IS HAPPENING");
            PlayerStats player = other.GetComponent<PlayerStats>();
            player.TakeDamage(100);
        }
    }
}


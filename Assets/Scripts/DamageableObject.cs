using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;

    public void TakeDamage(float damage)
    {
        //show particle effects, 
        //health bar?
        // do damage feedback
        health -= damage;
        if (health <= 0)
        { 
            Destroy(gameObject);
        }
    }
}


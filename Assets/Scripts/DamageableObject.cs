using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    public GameObject explosionEffectPrefab;
    private bool isDead = false;

    public void TakeDamage(float damage)
    {
        //show particle effects, 
        //health bar?
        // do damage feedback
        health -= damage;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            SpawnExplosionEffect();
            // Destroy the enemy object
            Destroy(gameObject);
        }
    }
    public bool IsDead()
    {
        return isDead;
    }
    private void SpawnExplosionEffect()
    {
        // Instantiate the explosion effect
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }
}


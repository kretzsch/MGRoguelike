using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    public GameObject explosionEffectPrefab;
    [SerializeField] private bool isEnemy;
    private bool _isDead = false;
    private StoreChildren _storeChildren;

    public void Start()
    {
        if (isEnemy) _storeChildren = transform.parent.GetComponent<StoreChildren>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !_isDead)
        {
            _isDead = true;
            SpawnExplosionEffect();
            if (isEnemy)
            {
                _storeChildren.RemoveChild(gameObject);
            }
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return _isDead;
    }

    private void SpawnExplosionEffect()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }
}


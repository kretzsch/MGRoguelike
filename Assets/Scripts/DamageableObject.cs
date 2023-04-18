using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    public GameObject explosionEffectPrefab;
    [SerializeField] private bool isEnemy;
    private bool _isDead = false;
    private StoreChildren _storeChildren;
    [SerializeField] private Manual2DFmod manual2DFmod;

    public void Start()
    {
        if (isEnemy) _storeChildren = transform.parent.GetComponent<StoreChildren>();

    }

    public void TakeDamage(float damage)
    {

        //show particle effects, 
        //health bar?
        // do damage feedback
        health -= damage;
        if (health <= 0 && !_isDead)
        {
            _isDead = true;
            SpawnExplosionEffect();
            if (isEnemy)
            {
                _storeChildren.RemoveChild(gameObject);
                CheckAllEnemiesDead();
            }
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Debug.Log("Enemy killed");
        if (isEnemy) CheckAllEnemiesDead();
    }
    public bool IsDead()
    {
        return _isDead;
    }

    public void CheckAllEnemiesDead()
    {
        if (_storeChildren.AllEnemiesDead())
        {
            //TEMP solution for playtest. 
            manual2DFmod.SetFmodParameter("level", "level2");
            Debug.Log("All enemies are dead.");
        }
    }
    private void SpawnExplosionEffect()
    {
        // Instantiate the explosion effect
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

}


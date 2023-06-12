using UnityEngine;

[RequireComponent(typeof(Renderer))]

///<summary>
/// DamageableObject is a script that can be attached to any GameObject to make it damageable
/// and react to damage, such as losing health and creating an explosion effect when destroyed.
/// </summary>
public abstract class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] protected int health = 100;
    public GameObject explosionEffectPrefab;
    [SerializeField] protected bool isEnemy;
    protected bool _isDead = false;
    protected EnemyManager _enemyManager;

    public void Start()
    {
        if (isEnemy) _enemyManager = transform.parent.GetComponent<EnemyManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !_isDead)
        {
            _isDead = true;
            SpawnExplosionEffect();
            if (isEnemy)
            {
                _enemyManager.UnregisterEnemy(gameObject);
            }
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return _isDead;
    }

    protected void SpawnExplosionEffect()
    {
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    // Define the method for handling collision in the derived classes
    public abstract void HandleCollision(GameObject other);
}

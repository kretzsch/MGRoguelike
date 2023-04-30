using UnityEngine;

[RequireComponent(typeof(Renderer))]

///<summary>
/// DamageableObject is a script that can be attached to any GameObject to make it damageable
/// and react to damage, such as losing health and creating an explosion effect when destroyed.
/// </summary>
public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;
    public GameObject explosionEffectPrefab;
    [SerializeField] private bool isEnemy;
    private bool _isDead = false;
    private EnemyManager _enemyManager;

    public void Start()
    {
        // If this object is an enemy, get the StoreChildren component from its parent
        if (isEnemy) _enemyManager = transform.parent.GetComponent<EnemyManager>();
    }

    // Apply damage to the object, check if it's dead, and trigger the appropriate reaction
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

    // Return whether the object is dead or not
    public bool IsDead()
    {
        return _isDead;
    }

    // Instantiate the explosion effect when the object is destroyed
    private void SpawnExplosionEffect()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

    // Handle 3D collisions
    private void OnCollisionEnter(Collision collision)
    {
        // Handle 3D collision logic here
    }

    // Handle 2D collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle 2D collision logic here
    }
}

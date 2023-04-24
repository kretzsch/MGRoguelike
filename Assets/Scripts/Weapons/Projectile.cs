using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Apply damage
            damageable.TakeDamage(damage);
        }
        // Destroy projectile
        Destroy(gameObject);
    }
}


using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100; // The maximum health of the enemy
    private float currentHealth; // The current health of the enemy
    [SerializeField]
    private float knockbackForce = 2.0f; // The base knockback force of the enemy
    private Rigidbody2D rb; // The Rigidbody2D component of the enemy

    private void Start()
    {
        currentHealth = maxHealth; // Initialize the current health
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    // This method is called when the enemy is hit
    public void TakeDamage(float damage, Vector2 direction)
    {
        currentHealth -= damage; // Decrease the health by the damage amount

        // If the health drops to 0 or below, call the Die method
        if (currentHealth <= 0)
        {
            Die();
        }

        // Apply the knockback
        ApplyKnockback(direction);
    }

    // This method handles the knockback effect
    public void ApplyKnockback(Vector2 direction)
    {
        // Apply the knockback force in the opposite direction of the hit
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
    }

    // This method is called when the enemy dies
    private void Die()
    {
        // Here you can add whatever should happen when the enemy dies
        // For now we'll just destroy the enemy object
        Destroy(gameObject);
    }
}

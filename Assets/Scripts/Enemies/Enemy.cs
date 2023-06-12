using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth;

    [Header("Knockback")]
    [SerializeField]
    private float knockbackForce = 2.0f;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField]
    private float speed = 2f;
    private Vector2 direction;

    [Header("Destruction")]
    public GameObject explosionEffectPrefab;
    private bool _isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.left; // The enemy will start moving to the left. Change as needed.
    }

    void Update()
    {
        if (!_isDead)
        {
            Move();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(10); // or however much damage you want to inflict
        }
    }
    public void TakeDamage(int damage, Vector2 direction)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

        ApplyKnockback(direction);
    }
    public void ApplyKnockback(Vector2 direction)
    {
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
    }

    public bool IsDead()
    {
        return _isDead;
    }

    private void Die()
    {
        _isDead = true;
        SpawnExplosionEffect();
        Destroy(gameObject);
    }

    private void SpawnExplosionEffect()
    {
        Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}

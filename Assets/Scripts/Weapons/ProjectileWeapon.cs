using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject bulletPrefab;

    public int damage;
    private float nextFireTime;

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            if (bulletPrefab == null)
            {
                return;
            }

            GameObject projectile = Instantiate(bulletPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            // Add the Projectile script and set the damage
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript == null)
            {
                projectileScript = projectile.AddComponent<Projectile>();
            }
            projectileScript.damage = damage;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = Quaternion.Euler(0, 0, -90f) * (projectileSpawnPoint.up) * projectileSpeed;


            currentAmmo--;
            nextFireTime = Time.time + 1f / fireRate;
            // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);
        }
    }

    // Handle 3D collisions
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Apply damage
            damageable.TakeDamage(weaponData.damage);
            // Destroy projectile
            Destroy(gameObject);
        }
    }

    // Handle 2D collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // Apply damage
            damageable.TakeDamage(weaponData.damage);
            // Destroy projectile
            Destroy(gameObject);
        }
    }
}

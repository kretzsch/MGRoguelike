using UnityEngine;
/// <summary>
/// A class that inherits from the Weapon class, representing projectile-based weapons.
/// </summary>
public class ProjectileWeapon : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    private float nextFireTime;

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = projectileSpawnPoint.up * projectileSpeed;

            currentAmmo--;
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}

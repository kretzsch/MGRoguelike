using UnityEngine;

/// <summary>
/// This class represents a weapon that fires projectiles. 
/// It contains methods for shooting, reloading, and managing ammo. 
/// It also has a reference to WeaponData, which holds information about the weapon's properties, 
/// such as damage, fire rate, and compatible ammo.
/// </summary>

public class ProjectileWeapon : Weapon
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool is3D;

    public int damage;
    private float nextFireTime;

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            if (is3D)
            {
                Shoot3D();
            }
            else
            {
                Shoot2D();
            }

            nextFireTime = Time.time + 1f / fireRate;
            currentAmmo--;
            LoadoutData.UpdateAmmoCount(weaponData.compatibleAmmo.ammoName, currentAmmo, weaponData.magazineSize);
            Debug.Log($"Shoot - AmmoName: {weaponData.compatibleAmmo.ammoName}, CurrentAmmo: {currentAmmo}");
        }
    }

    private void Shoot2D()
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

        // Destroy the projectile after 3 seconds
        Destroy(projectile, 3f);
    }

    private void Shoot3D()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 500f))
        {
            // Apply damage to hit object
            IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
            }
        }
    }
}

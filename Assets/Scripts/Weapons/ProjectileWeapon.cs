using System.Collections;
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
    [SerializeField] private bool useRaycast;
    [SerializeField] private ParticleSystem gunShootParticles;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private Transform firePoint;

    public int damage;
    private float nextFireTime;
    private float lastFireTime;

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            if (useRaycast)
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
            lastFireTime = Time.time;
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 500f))
            {
                // Instantiate bulletTrail and play shoot particle fx
                gunShootParticles.Play();
                bulletTrail.transform.position = firePoint.transform.position;

                StartCoroutine(MoveBulletTrail(bulletTrail, bulletTrail.transform.position, bulletTrail.transform.position + bulletTrail.transform.forward * 7, fireRate));

                // Change the color of the object hit
                Renderer hitObjectRenderer = hit.collider.GetComponent<Renderer>();
                IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();

                if (hitObjectRenderer != null && damageableObject != null)
                {
                    StartCoroutine(FlickerAndResetColor(hitObjectRenderer, damageableObject, Color.red, 0.05f, 2));
                }

                // Apply damage to hit object
                if (damageableObject != null)
                {
                    damageableObject.TakeDamage(damage);
                }
            }
    }

    private IEnumerator FlickerAndResetColor(Renderer renderer, IDamageable damageable, Color flickerColor, float flickerDuration, int flickerCount)
    {
        Color originalColor = renderer.material.color;

        for (int i = 0; i < flickerCount; i++)
        {
            if (renderer != null && damageable != null && !damageable.IsDead())
            {
                renderer.material.color = flickerColor;
                yield return new WaitForSeconds(flickerDuration);
                if (renderer != null)
                {
                    renderer.material.color = originalColor;
                }
            }
            yield return new WaitForSeconds(flickerDuration);
        }
    }
    private IEnumerator MoveBulletTrail(GameObject bulletTrail, Vector3 startPos, Vector3 endPos, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            bulletTrail.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bulletTrail.transform.position = endPos;
    }
}
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
    [Header("2D")]
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject bulletPrefab;//2D

    [Header("3D")]
    private GameObject _mainCamera;
    [SerializeField] private bool is3D;
    [SerializeField] private ParticleSystem gunShootParticles;
    [SerializeField] private GameObject bulletTrail; //3D
    [SerializeField] private Transform firePoint; //3D

    [Header("global")]
    public int damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletTrailSpeed;
    private float nextFireTime;
    private float lastFireTime;


    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        Debug.Log($"Main Camera assigned: {_mainCamera}");
    }
    public override void Shoot()
    {
        Debug.Log("Shoot method called"); 


        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            Debug.Log("Shoot method : first if"); 


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
        Debug.Log("Shoot2D method called");
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
        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit, 500f))
        {
            PlayShootEffects();

            IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
            Renderer hitObjectRenderer = hit.collider.GetComponent<Renderer>();

            if (hitObjectRenderer != null && damageableObject != null)
            {
                StartCoroutine(FlickerAndResetColor(hitObjectRenderer, damageableObject, Color.red, 0.05f, 2));
            }

            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
            }
        }
    }

    private void PlayShootEffects()
    {
        gunShootParticles.Play();
        GameObject newBulletTrail = Instantiate(bulletTrail, firePoint.transform.position, firePoint.transform.rotation);
        StartCoroutine(MoveBulletTrail(newBulletTrail, newBulletTrail.transform.position, newBulletTrail.transform.position + newBulletTrail.transform.forward * bulletTrailSpeed, fireRate));
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
        Destroy(bulletTrail, duration);
    }

}
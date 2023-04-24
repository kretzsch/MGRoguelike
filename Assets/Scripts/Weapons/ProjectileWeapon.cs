using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float fireRate;
    private float nextFireTime;

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && currentAmmo > 0)
        {
            //right now the levelvisuals is hardcoded, this needs to be worked on
            Sprite projectileSprite = weaponData.weaponVisualsData.levelVisuals[0].sprite;
            GameObject projectile = new GameObject("Projectile");
            projectile.AddComponent<SpriteRenderer>().sprite = projectileSprite;
            projectile.AddComponent<Rigidbody2D>().gravityScale = 0;
            projectile.AddComponent<BoxCollider2D>().isTrigger = true;
            projectile.transform.position = projectileSpawnPoint.position;
            projectile.transform.rotation = projectileSpawnPoint.rotation;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = projectileSpawnPoint.up * projectileSpeed;

            currentAmmo--;
            nextFireTime = Time.time + 1f / fireRate;
            // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);
        }
    }
}

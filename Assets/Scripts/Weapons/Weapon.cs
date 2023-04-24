using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    protected int currentAmmo;

    public abstract void Shoot();

    public void SetAmmo(int ammo)
    {
        currentAmmo = Mathf.Clamp(ammo, 0, weaponData.magazineSize);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}

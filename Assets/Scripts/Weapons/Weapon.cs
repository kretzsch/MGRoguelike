using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    protected int currentAmmo;

    public abstract void Shoot();

    public virtual void Reload()
    {
        if (weaponData != null)
        {
            string ammoName = weaponData.compatibleAmmo.ammoName;

            if (LoadoutData.remainingAmmo.ContainsKey(ammoName))
            {
                int reloadAmount = Mathf.Min(LoadoutData.remainingAmmo[ammoName], weaponData.magazineSize - currentAmmo);
                currentAmmo += reloadAmount;
                LoadoutData.remainingAmmo[ammoName] -= reloadAmount;
                LoadoutData.UpdateAmmoCount(weaponData.compatibleAmmo.ammoName, currentAmmo, weaponData.magazineSize);
            }
        }
    }
    public void SetAmmo(int ammo)
    {
        currentAmmo = Mathf.Clamp(ammo, 0, weaponData.magazineSize);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}

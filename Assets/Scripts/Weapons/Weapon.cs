using UnityEngine;

/// <summary>
/// This abstract class represents a generic weapon with common properties and methods. 
/// It holds a reference to the WeaponData and manages the current ammo count. 
/// The Shoot method is defined as abstract,
/// meaning that derived classes must implement their own version of the method. 
/// The Reload method is virtual, allowing derived classes to override it if necessary. 
/// Helper methods to set and get the current ammo are also included.
/// </summary>
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

    public void InitializeAmmo()
    {
        if (weaponData != null)
        {
            string ammoName = weaponData.compatibleAmmo.ammoName;

            if (LoadoutData.selectedWeaponsAndAmmo.ContainsKey(ammoName))
            {
                int ammoToLoad = Mathf.Min(LoadoutData.selectedWeaponsAndAmmo[ammoName], weaponData.magazineSize);
                SetAmmo(ammoToLoad);
                LoadoutData.remainingAmmo[ammoName] -= ammoToLoad;
                LoadoutData.UpdateAmmoCount(ammoName, currentAmmo, LoadoutData.remainingAmmo[ammoName]);
            }
        }
    }

}

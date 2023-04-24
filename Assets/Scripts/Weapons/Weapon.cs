using UnityEngine;
/// <summary>
/// The base class for all weapons in the game. It should be inherited and extended by specific weapon types.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int maxAmmo;
    protected int currentAmmo;

    public abstract void Shoot();

    public void SetAmmo(int ammo)
    {
        currentAmmo = Mathf.Clamp(ammo, 0, maxAmmo);
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}

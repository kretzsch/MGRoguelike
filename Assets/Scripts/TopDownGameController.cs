using UnityEngine;

public class TopDownGameController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    private void Start()
    {
        // Pass the weapons and ammo data to the WeaponManager
        weaponManager.SetupWeapons();
    }
}

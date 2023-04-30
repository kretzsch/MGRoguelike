using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// this class is reuseable for every genre. 
/// The gamecontroller gets the weapon and ammo data from the loadoutdata thats saved between scenes.
/// this in turn calls the weaponmanager.setupweapons to set up the weapons with the loaded data.
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private WeaponData.GameGenre currentGenre;
    private WeaponManager _weaponManager;
    private LevelManager _levelManager;
    private EnemyManager _enemyManager;


    private void Awake()
    {
        _weaponManager = FindObjectOfType<WeaponManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _enemyManager = FindObjectOfType<EnemyManager>();
        Debug.Log($"WeaponManager reference: {_weaponManager}");
    }

    private void Start()
    {
        // Get the weapons and ammo data from LoadoutData
        Dictionary<string, int> weaponsAndAmmo = LoadoutData.selectedWeaponsAndAmmo;

        // Debug the content of the dictionary
        Debug.Log("LoadoutData content: " + string.Join(", ", weaponsAndAmmo.Select(kv => kv.Key + ": " + kv.Value).ToArray()));

        // Pass the weapons and ammo data to the WeaponManager
        _weaponManager.SetupWeapons(weaponsAndAmmo, currentGenre);

        // Subscribe to EnemyManager's event


        if (_levelManager != null && _enemyManager != null)
        {
            _levelManager.SubscribeToEnemyManager(_enemyManager);
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from EnemyManager's event
        _levelManager = FindObjectOfType<LevelManager>();
        _enemyManager = FindObjectOfType<EnemyManager>();

        if (_levelManager != null && _enemyManager != null)
        {
            _levelManager.UnsubscribeFromEnemyManager(_enemyManager);
        }
    }
}


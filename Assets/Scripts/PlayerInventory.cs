using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private Dictionary<string, int> weaponsAndAmmo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetInventory(Dictionary<string, int> newInventory)
    {
        weaponsAndAmmo = newInventory;
    }

    public Dictionary<string, int> GetInventory()
    {
        return weaponsAndAmmo;
    }

    // Add other methods to manage the player's inventory, such as adding or removing items, as needed.
}

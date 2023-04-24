using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StoreChildren is responsible for keeping track of all child objects (enemies) in the scene.
/// It triggers an event when all child objects are removed (enemies are dead).
/// </summary>
public class StoreChildren : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    // Delegate and event for when all enemies are dead
    public delegate void OnAllEnemiesDead();
    public event OnAllEnemiesDead OnAllEnemiesDeadEvent;

    // List of child GameObjects
    private List<GameObject> _children;

    private void Awake()
    {
        _children = new List<GameObject>();
    }

    private void Start()
    {
        // Add all children to the list
        foreach (Transform child in transform)
        {
            _children.Add(child.gameObject);
        }
    }

    // Remove a child from the list and check if all enemies are dead
    public void RemoveChild(GameObject child)
    {
        _children.Remove(child);
        CheckAllEnemiesDead();
    }

    // Check if all enemies are dead
    public bool AllEnemiesDead()
    {
        return _children.Count == 0;
    }

    // Trigger the OnAllEnemiesDeadEvent if all enemies are dead
    private void CheckAllEnemiesDead()
    {
        if (AllEnemiesDead())
        {
            OnAllEnemiesDeadEvent?.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreChildren : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public delegate void OnAllEnemiesDead();
    public event OnAllEnemiesDead OnAllEnemiesDeadEvent;

    private List<GameObject> _children;

    private void Awake()
    {
        _children = new List<GameObject>();
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _children.Add(child.gameObject);

        }
    }

    public void RemoveChild(GameObject child)
    {
        _children.Remove(child);
        CheckAllEnemiesDead();
    }

    public bool AllEnemiesDead()
    {
        return _children.Count == 0;
    }

    private void CheckAllEnemiesDead()
    {
        if (AllEnemiesDead())
        {
            OnAllEnemiesDeadEvent?.Invoke();
        }
    }
}

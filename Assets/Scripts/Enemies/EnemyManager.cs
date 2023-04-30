using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public delegate void OnAllEnemiesDead();
    public event OnAllEnemiesDead OnAllEnemiesDeadEvent;

    private List<GameObject> enemies;

    private void Awake()
    {
        enemies = new List<GameObject>();
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            enemies.Add(child.gameObject);
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        Debug.Log("RegisterEnemy called");
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        Debug.Log("UnRegisterEnemy called");
        enemies.Remove(enemy);
        CheckAllEnemiesDead();
    }

    private void CheckAllEnemiesDead()
    {
        if (enemies.Count == 0)
        {
            OnAllEnemiesDeadEvent?.Invoke();
        }
    }
}


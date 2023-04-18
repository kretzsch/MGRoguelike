using System.Collections.Generic;
using UnityEngine;

public class StoreChildren : MonoBehaviour
{
    private List<GameObject> childObjects;

    void Start()
    {
        childObjects = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects.Add(transform.GetChild(i).gameObject);
        }
    }

    public void RemoveChild(GameObject child)
    {
        childObjects.Remove(child);
    }

    public bool AllEnemiesDead()
    {
        return childObjects.Count == 0;
    }
}



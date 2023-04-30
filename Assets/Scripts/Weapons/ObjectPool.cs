using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}


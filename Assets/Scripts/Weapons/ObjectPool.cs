using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

    public void CreatePool(string tag, GameObject prefab, int magazineSize)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " already exists.");
            return;
        }

        Pool newPool = new Pool
        {
            tag = tag,
            prefab = prefab,
            size = magazineSize
        };

        pools.Add(newPool);

        Queue<GameObject> objectPool = new Queue<GameObject>();

        for (int i = 0; i < magazineSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(tag, objectPool);
    }

    public GameObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        // Check if the object is still active in the scene
        if (objectToSpawn == null || objectToSpawn.Equals(null))
        {
            // Create a new instance of the prefab and add it to the pool
            Pool pool = pools.Find(p => p.tag == tag);
            objectToSpawn = Instantiate(pool.prefab);
            objectToSpawn.SetActive(false);
            poolDictionary[tag].Enqueue(objectToSpawn);
        }

        objectToSpawn.SetActive(true);
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }


    public void ReturnObject(GameObject objectToReturn, string tag)
    {
        if (objectToReturn == null || !poolDictionary.ContainsKey(tag))
        {
            return;
        }

        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }

    public Pool GetPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        return pools.Find(pool => pool.tag == tag);
    }


}

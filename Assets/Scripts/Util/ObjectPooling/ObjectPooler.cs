// This script is responsible for object pooling.

#region Import Namespaces

using System.Collections.Generic;
using UnityEngine;

#endregion

#region Core Implementation

public class ObjectPooler : MonoBehaviour
{
    // Dictionary to store object pools with tags.
    public Dictionary<string, Queue<GameObject>> objectPoolDictionary;

    // List of pool configurations.
    public List<Pool> pools = new List<Pool>();

    // Static reference to the ObjectPooler instance.
    public static ObjectPooler Instance;

    // Singleton pattern implementation.
    private void Awake()
    {
        // Ensure only one instance of ObjectPooler exists.
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

    // Initialize object pool dictionary and pools on Start.
    private void Start()
    {
        objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        // Create object pools based on pool configurations.
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            Transform parent = new GameObject(pool.tag).transform;
            parent.gameObject.isStatic = true;

            // Instantiate objects and enqueue them into the pool.
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parent);
                obj.name = pool.tag;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            // Add object pool to the dictionary with its tag as the key.
            objectPoolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Spawn an object from the object pool at the specified position and rotation.
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // Check if the requested pool exists.
        if (!objectPoolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        // Dequeue an object from the pool and activate it.
        GameObject objectToSpawn = objectPoolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        // Enqueue the object back into the pool.
        objectPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    // Return an object to its respective object pool.
    public void ReturnToPool(GameObject objectToReturn)
    {
        // Deactivate the object and enqueue it back into its pool.
        objectToReturn.SetActive(false);
        string tag = objectToReturn.tag;
        objectPoolDictionary[tag].Enqueue(objectToReturn);
    }
}

#endregion
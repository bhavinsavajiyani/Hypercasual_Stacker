using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> objectPoolDictionary;
    public List<Pool> pools = new List<Pool>();

    public static ObjectPooler Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            Transform parent = new GameObject(pool.tag).transform;
            parent.gameObject.isStatic = true;

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parent);
                obj.name = pool.tag + i;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            objectPoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!objectPoolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool with tag " + tag + " doesn't exist!");
            return null;
        }

        GameObject objectToSpawn = objectPoolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        Debug.Log("Returning " + objectToReturn.name + " to pool...");
        objectToReturn.SetActive(false);
        string tag = objectToReturn.tag;
        objectPoolDictionary[tag].Enqueue(objectToReturn);
    }
}

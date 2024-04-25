// This script represents a pool configuration for object pooling.

#region Import Namespaces

using UnityEngine;

#endregion

#region Core Implementation

[System.Serializable]
public class Pool
{
    // Tag to identify the pool.
    public string tag;

    // Prefab of the object to be pooled.
    public GameObject prefab;

    // Initial size of the object pool.
    public int size;
}

#endregion
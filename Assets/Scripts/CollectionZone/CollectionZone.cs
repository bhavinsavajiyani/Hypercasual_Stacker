// This script represents a collection zone where items can be collected.

#region Import Namespaces

using UnityEngine;

#endregion

#region Core Implementation

public class CollectionZone : MonoBehaviour
{
    // Name of the collection zone.
    public string zoneName;

    // Prefab of the item that can be collected in this zone.
    public GameObject itemPrefab;
}

#endregion
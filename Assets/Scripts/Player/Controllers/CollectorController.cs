// This script is responsible for controlling item collection and delivery.

#region Import Namespaces

using System.Collections.Generic;
using UnityEngine;

#endregion

#region Core Implementation

public class CollectorController : MonoBehaviour
{
    // Reference to the parent transform where collected items are held.
    public Transform holderParent;

    // Reference to the timer used for item collection.
    public Timer timer;

    // Time in seconds it takes to collect an item.
    [SerializeField] private int _itemCollectionTime = 2;

    // Flag indicating whether the collector is in the collection area.
    private bool _isInCollectionArea;

    // List to store collected items.
    private List<GameObject> _collectedItems = new List<GameObject>();

    // Reference to the collection zone.
    private CollectionZone _zone;

    // Reference to the item to be delivered.
    private GameObject _itemToDeliver;

    // Flag indicating whether the item has been delivered.
    private bool _itemDelivered;

    // Initialize timer with collection time on Awake.
    private void Awake()
    {
        timer.seconds = _itemCollectionTime;
    }

    // Handle trigger enter events.
    private void OnTriggerEnter(Collider other)
    {
        _zone = other.GetComponent<CollectionZone>();
        if (_zone != null)
        {
            _isInCollectionArea = true;
        }

        // If collided with a customer, deliver the requested item.
        if (other.CompareTag("Customer"))
        {
            CustomerBehaviour customer = other.GetComponent<CustomerBehaviour>();
            _itemToDeliver = customer.itemWanted;
            DeliverItem(_itemToDeliver);
            customer.SetItemProvidedStatus(_itemDelivered);
        }
    }

    // Handle trigger exit events.
    private void OnTriggerExit(Collider other)
    {
        _zone = other.GetComponent<CollectionZone>();
        if (_zone != null)
        {
            _isInCollectionArea = false;
        }
    }

    // Update is called once per frame.
    private void Update()
    {
        // Activate timer if the collector is in the collection area.
        timer.gameObject.SetActive(_isInCollectionArea);

        // Reset timer if collector is not in the collection area.
        if (!_isInCollectionArea)
        {
            timer.ResetTimer();
        }

        // Collect the item if the timer runs out and the collector is in the collection area.
        if (timer.timeRemaining == 0 && _isInCollectionArea)
        {
            CollectItem(_zone.itemPrefab);
        }
    }

    // Method to collect an item and add it to the collector's inventory.
    private void CollectItem(GameObject itemPrefab)
    {
        // Spawn the item from the object pool and place it in the holder parent.
        GameObject item = ObjectPooler.Instance.SpawnFromPool(itemPrefab.name, holderParent.position, holderParent.rotation);
        item.transform.SetParent(holderParent);
        item.transform.localPosition = new Vector3(0, _collectedItems.Count * 0.6f, 0.1f);
        _collectedItems.Insert(0, item);
        timer.StartTimer();
    }

    // Method to deliver an item to a customer.
    private void DeliverItem(GameObject item)
    {
        // Find the item to be delivered in the collected items list.
        GameObject itemToBeDelivered = _collectedItems.Find(obj => obj.tag == item.tag);
        if (itemToBeDelivered != null)
        {
            // Mark the item as delivered, return it to the object pool, and remove it from the collected items list.
            _itemDelivered = true;
            ObjectPooler.Instance.ReturnToPool(itemToBeDelivered);
            _collectedItems.Remove(itemToBeDelivered);
        }
        else
        {
            // If the requested item is not found, mark delivery as unsuccessful.
            _itemDelivered = false;
        }
    }
}

#endregion
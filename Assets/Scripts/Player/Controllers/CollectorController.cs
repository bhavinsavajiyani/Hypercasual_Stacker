using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorController : MonoBehaviour
{
    public Transform holderParent;
    public Timer timer;

    [SerializeField] private int _itemCollectionTime = 1;
    private bool _isInCollectionArea;
    private List<GameObject> _collectedItems = new List<GameObject>();

    private CollectionZone _zone;

    private void Awake()
    {
        timer.seconds = _itemCollectionTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        _zone = other.GetComponent<CollectionZone>();
        if(_zone != null)
        {
            Debug.Log("Entering " + _zone.name + " Area...");
            _isInCollectionArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _zone = other.GetComponent<CollectionZone>();
        if (_zone != null)
        {
            Debug.Log("Exiting " + _zone.name + " Area...");
            _isInCollectionArea = false;
        }
    }

    private void Update()
    {
        timer.gameObject.SetActive(_isInCollectionArea);

        if(!_isInCollectionArea)
        {
            timer.ResetTimer();
        }

        if (timer.timeRemaining <= 0 && _isInCollectionArea)
        {
            CollectItem(_zone.itemPrefab);
        }
    }

    private void CollectItem(GameObject itemPrefab)
    {
        GameObject item = ObjectPooler.Instance.SpawnFromPool(itemPrefab.name, holderParent.position, holderParent.rotation);
        item.transform.SetParent(holderParent);
        item.transform.localPosition = new Vector3(0, _collectedItems.Count * 0.6f, 0.1f);
        _collectedItems.Add(item);
        timer.StartTimer();
    }

    private void DeliverItem(GameObject item)
    {
        
    }
}

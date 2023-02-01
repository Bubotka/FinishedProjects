using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ClearCounter _secondCounter;
    [SerializeField] private bool _testing;

    private KitchenObject _kitchenObject;

    private void Update()
    {
        if(_testing&&Input.GetKeyDown(KeyCode.T))
        {
            if(_kitchenObject != null)
            {
                _kitchenObject.SetClearCounter(_secondCounter);
            }
        }
    }

    public void Interact()
    {
        if (_kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO._prefab, _spawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(_kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _spawnPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject = _kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}

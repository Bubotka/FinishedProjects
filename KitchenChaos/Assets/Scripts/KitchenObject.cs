using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    private ClearCounter _clearCounter;

    public KitchenObjectSO GetKitcheObjectSO()
    {
        return _kitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (clearCounter != null)
        {
            clearCounter.ClearKitchenObject();
        }
        _clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a kitchenObject!");
        }
        _clearCounter.SetKitchenObject(this);
        transform.parent=_clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return _clearCounter;
    }
}

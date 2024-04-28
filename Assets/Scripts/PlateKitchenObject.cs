using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;

    private List<KitchenObjectSO> _kitchenObjectsSOList;

    private void Awake()
    {
        _kitchenObjectsSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!_validKitchenObjectSOList.Contains(kitchenObjectSO)) 
            return false;

        if (_kitchenObjectsSOList.Contains(kitchenObjectSO))
            return false;

        _kitchenObjectsSOList.Add(kitchenObjectSO);
        return true;
    }
}
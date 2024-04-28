using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

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

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs 
        { 
            KitchenObjectSO = kitchenObjectSO 
        });

        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return _kitchenObjectsSOList;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO _recipeListSO;
    [SerializeField] private float _spawnRecipeTimerMax = 4f;
    [SerializeField] private int _waitingRecipesMax = 4;

    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;

    private void Awake()
    {
        Instance = this;

        _waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;

        if (_spawnRecipeTimer <= 0)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count < _waitingRecipesMax)
            {
                RecipeSO waitingRecipeSo = _recipeListSO.RecipeSOList[Random.Range(0, _recipeListSO.RecipeSOList.Count)];
                Debug.Log(waitingRecipeSo.recipeName);
                _waitingRecipeSOList.Add(waitingRecipeSo);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i=0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSo = _waitingRecipeSOList[i];

            if (waitingRecipeSo.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // Has the same number of ingredients
                bool platesContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSo.KitchenObjectSOList)
                {
                    // Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        // Cycling through all ingredients on the Plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            // Ingredients match
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (ingredientFound == false)
                    {
                        // This Recipe ingredient was not found on the Plate
                        platesContentMatchesRecipe = false;
                    }
                }

                if (platesContentMatchesRecipe)
                {
                    // Player delivered the correct recipe
                    Debug.Log("Player delivered the correct recipe!");
                    _waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }

        // No matches found!
        // Player did not deliver a correct recipe
        Debug.Log("Player did not deliver a correct recipe!");
    }
}
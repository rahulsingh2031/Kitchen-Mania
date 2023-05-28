using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event System.Action OnRecipeSpawned;
    public event System.Action OnRecipeComplete;
    public event System.Action OnRecipeSuccess;
    public event System.Action OnRecipeFailed;


    public static DeliveryManager Instance { get; private set; }
    private List<RecipeSO> waitingRecipeSOList = new List<RecipeSO>();
    [SerializeField] private RecipeListSO recipeListSO;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfullDeliveryRecipe = 0;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {

            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke();
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {   //Taking each recipe In WaitingList
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            //checking if  plate have same no of time as currentIterated Recipe
            if (waitingRecipeSO.kitchenObjectList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {

                bool plateContentsMatchesRecipe = true;
                //Cycling through all recipeKitchenObject and checking ,does plate have all recipeItem or not
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectList)
                {
                    //if plate does contain currentRecipeItem
                    if (plateKitchenObject.GetKitchenObjectSOList().Contains(recipeKitchenObjectSO))
                    {

                    }
                    //if plate does not  contain currentRecipeItem and move to next waitingRecipeList
                    else
                    {
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //Player Deliver Correct Recipe
                    // print("Player Delivers Correct Recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeComplete?.Invoke();
                    OnRecipeSuccess?.Invoke();
                    successfullDeliveryRecipe++;
                    return;
                }
            }
        }

        OnRecipeFailed?.Invoke();
        //No Matches Found 
        // print("Player  Does not Delivers Correct Recipe");


    }
    public int GetSuccessfulRecipeDelivery()
    {
        return successfullDeliveryRecipe;
    }
    public List<RecipeSO> GetWaitingRecipeSOList() => waitingRecipeSOList;
}

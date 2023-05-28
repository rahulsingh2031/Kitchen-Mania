using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event System.Action<State> OnStateChanges;
    public event Action<float> OnProgressChanged;

    public enum State
    {
        Idle, Frying, Fried, Burned
    }
    private State state;
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOs;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOs;
    private float fryingTimer, burningTimer;
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {

        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(fryingTimer / fryingRecipeSO.fryingTimerMax);
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax)
                    {

                        GetKitchenObject().DestroySelf();
                        KitchenObject.Construct(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        OnStateChanges?.Invoke(state);
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(burningTimer / burningRecipeSO.BurningTimerMax);
                    if (burningTimer > burningRecipeSO.BurningTimerMax)
                    {

                        GetKitchenObject().DestroySelf();
                        KitchenObject.Construct(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanges?.Invoke(state);

                        //Setting 0f since we dont want to show progressBar at this state now
                        OnProgressChanged?.Invoke(0f);
                    }
                    break;
                case State.Burned:
                    break;
            }

        }
    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    // cuttingProgress = 0;

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    OnStateChanges?.Invoke(state);
                    fryingTimer = 0f;
                    OnProgressChanged?.Invoke(fryingTimer / fryingRecipeSO.fryingTimerMax);


                }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {

                        GetKitchenObject().DestroySelf();
                        OnProgressChanged?.Invoke(0f);

                        // GetKitchenObject().SetKitchenObjectParent(player);

                        state = State.Idle;
                        OnStateChanges?.Invoke(state);
                    }
                }

            }
            else if (!player.HasKitchenObject())
            {

                OnProgressChanged?.Invoke(0f);
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanges?.Invoke(state);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {

        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }


    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOs)
        {

            if (inputKitchenObjectSO == fryingRecipeSO.input)
            {

                return fryingRecipeSO;
            }
        }
        return null;

    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOs)
        {

            if (inputKitchenObjectSO == burningRecipeSO.input)
            {

                return burningRecipeSO;
            }
        }
        return null;

    }
}

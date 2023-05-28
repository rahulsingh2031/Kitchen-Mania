using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event Action OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {

        if (!player.HasKitchenObject())
        {
            KitchenObject.Construct(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke();
        }


    }



}

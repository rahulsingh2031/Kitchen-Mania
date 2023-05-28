using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event System.Action OnPlateSpawned;
    public event System.Action OnPlateRemoved;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimerMax = 4f;
    private float spawnPlateTimer;

    private int spawnPlatesAmount;
    private int spawnPlatesAmountMax = 4;
    void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (spawnPlatesAmount <= spawnPlatesAmountMax)
            {
                spawnPlatesAmount++;
                OnPlateSpawned?.Invoke();

            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (spawnPlatesAmount > 0)
            {
                KitchenObject.Construct(plateKitchenObjectSO, player);
                spawnPlatesAmount--;
                OnPlateRemoved?.Invoke();
            }
        }
    }
}

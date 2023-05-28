using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [System.Serializable]
    private class KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject kitchenGameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += ShowVisualOnIngredientAdd;
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList)
        {

            kitchenObjectSO_GameObject.kitchenGameObject.SetActive(false);

        }
    }

    private void ShowVisualOnIngredientAdd(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSO_GameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSO_GameObject.KitchenObjectSO == kitchenObjectSO)
            {
                kitchenObjectSO_GameObject.kitchenGameObject.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image image;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}

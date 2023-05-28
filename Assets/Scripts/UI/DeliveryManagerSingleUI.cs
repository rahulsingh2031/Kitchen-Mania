using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform childTransform in iconContainer)
        {
            if (childTransform == iconTemplate) continue;
            Destroy(childTransform.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectList)
        {
            Transform kitchenObjectIconTransform = Instantiate(iconTemplate, iconContainer);
            kitchenObjectIconTransform.gameObject.SetActive(true);
            kitchenObjectIconTransform.GetComponent<UnityEngine.UI.Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}

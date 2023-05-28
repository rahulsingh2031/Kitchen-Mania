using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI countdownUI;

    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void GameManager_OnStateChange()
    {

        if (GameManager.Instance.IsGameOver())
        {

            Show();
            countdownUI.text = DeliveryManager.Instance.GetSuccessfulRecipeDelivery().ToString();
        }
        else
        {
            Hide();
        }
    }


    private void Show()
    {

        gameObject.SetActive(true);
    }
    private void Hide()
    {

        gameObject.SetActive(false);
    }
}

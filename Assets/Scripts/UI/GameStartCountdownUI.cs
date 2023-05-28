using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownUI;
    private void Start()
    {
        GameManager.Instance.OnStateChange += GameManager_OnStateChange;
        Hide();
    }

    private void GameManager_OnStateChange()
    {

        if (GameManager.Instance.IsCountdownTimerActive())
        {

            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        // print("gamestate.Countdown:      " + GameManager.Instance.IsCountdownTimerActive());
        countdownUI.text = Mathf.Ceil(GameManager.Instance.GetCountdownStartTime()).ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image timerImage;
    private void Awake()
    {
        timerImage.fillAmount = 0f;
    }
    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
            timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}

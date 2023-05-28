using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event System.Action OnStateChange;
    public static GameManager Instance { get; private set; }
    private enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    private GameState state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 0f;
    private float gamePlayingTimerMax = 10f;
    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;
        state = GameState.WaitingToStart;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += PauseUnpause;
    }
    private void Update()
    {
        switch (state)
        {
            case GameState.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    state = GameState.CountdownToStart;
                    OnStateChange?.Invoke();
                }
                break;

            case GameState.CountdownToStart:

                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0)
                {
                    state = GameState.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChange?.Invoke();
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    state = GameState.GameOver;
                    OnStateChange?.Invoke();
                }
                break;
            case GameState.GameOver:

                break;
        }

    }

    public void GetStateName()
    {
        print(state);
    }
    public bool IsGamePlaying()
    {
        return state == GameState.GamePlaying;
    }
    public bool IsCountdownTimerActive()
    {
        return state == GameState.CountdownToStart;
    }
    public bool IsGameOver()
    {
        return state == GameState.GameOver;
    }

    public float GetCountdownStartTime() => countdownToStartTimer;

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    private void PauseUnpause()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
            Time.timeScale = 0f;
        else
        {
            Time.timeScale = 1f;
        }
    }
}

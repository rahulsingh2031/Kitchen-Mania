using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private void Awake()
    {
        stoveCounter = GetComponentInParent<StoveCounter>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanges += StoveCounter_OnStateChanges;
    }

    private void StoveCounter_OnStateChanges(StoveCounter.State state)
    {
        bool playSound = state == StoveCounter.State.Frying || state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}

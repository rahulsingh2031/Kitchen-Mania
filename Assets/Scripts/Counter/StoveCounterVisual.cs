using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private GameObject StoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    StoveCounter stoveCounter;
    private void Start()
    {
        StoveOnGameObject.SetActive(false);
        particlesGameObject.SetActive(false);
        stoveCounter = GetComponentInParent<StoveCounter>();
        stoveCounter.OnStateChanges += UpdateVisualOnStateChange;
    }

    private void UpdateVisualOnStateChange(StoveCounter.State state)
    {
        bool showVisual = state == StoveCounter.State.Frying || state == StoveCounter.State.Fried;
        StoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArray;
    void Start()
    {

        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // Update is called once per frame
    void Player_OnSelectedCounterChanged(BaseCounter selectedCounter)
    {
        if (selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    private void Show()
    {
        foreach (var visualGameObject in visualGameObjectArray)
        {

            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {

        foreach (var visualGameObject in visualGameObjectArray)
        {

            visualGameObject.SetActive(false);
        }
    }
}

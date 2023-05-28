using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    private PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;

    [SerializeField] private Transform plateVisual;
    List<GameObject> plateVisualGameObjectList = new List<GameObject>();
    private void Start()
    {
        plateCounter = GetComponentInParent<PlateCounter>();
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateSpawned()
    {
        Transform plateVisualTransform = Instantiate(plateVisual, counterTopPoint);
        float plateOffsetY = 0.11f;
        plateVisualTransform.localPosition += Vector3.up * plateOffsetY * plateVisualGameObjectList.Count;
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);


    }
    private void PlateCounter_OnPlateRemoved()
    {
        Destroy(plateVisualGameObjectList[plateVisualGameObjectList.Count - 1]);
        plateVisualGameObjectList.RemoveAt(plateVisualGameObjectList.Count - 1);
    }
}

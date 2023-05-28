using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipReferenceSO audioClipReferenceSO;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickupSomething += OnPickupSomething;
        BaseCounter.OnAnyObjectPlacedHere += OnAnyObjectPlacedOnCounter;
        TrashCounter.OnAnyObjectThrased += OnAnyObjectThrased;

    }
    private void OnPickupSomething()
    {
        PlaySound(audioClipReferenceSO.objectPickup, Player.Instance.transform.position);
    }
    private void OnAnyObjectPlacedOnCounter(BaseCounter baseCounter)
    {
        PlaySound(audioClipReferenceSO.objectDrop, baseCounter.transform.position);
    }
    private void OnAnyObjectThrased(TrashCounter trashCounter)
    {
        PlaySound(audioClipReferenceSO.trash, trashCounter.transform.position);
    }



    private void CuttingCounter_OnAnyCut()
    {
        PlaySound(audioClipReferenceSO.chop, Camera.main.transform.position);
    }
    private void DeliveryManager_OnRecipeSuccess()
    {
        PlaySound(audioClipReferenceSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed()
    {
        PlaySound(audioClipReferenceSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }

    public void PlayFootSound(Vector3 position, float volume)
    {
        PlaySound(audioClipReferenceSO.footStep, position, volume);

    }
}

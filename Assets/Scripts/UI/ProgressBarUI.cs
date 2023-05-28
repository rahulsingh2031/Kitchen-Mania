using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private IHasProgress hasProgress;
    [SerializeField] private Image imageBar;
    void Start()
    {
        hasProgress = GetComponentInParent<IHasProgress>();
        hasProgress.OnProgressChanged += UpdateProgressBarUI;
        imageBar.fillAmount = 0f;
        Hide();
    }

    private void UpdateProgressBarUI(float progressNormalized)
    {
        imageBar.fillAmount = progressNormalized;
        if (progressNormalized == 0f || progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}

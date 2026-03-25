using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTimerUI : MonoBehaviour
{
    public Image fillImage;

    float maxTime;

    public void Init(float time)
    {
        maxTime = time;

        gameObject.SetActive(true); 
        fillImage.fillAmount = 1f;
    }

    public void UpdateBar(float currentTime)
    {
        if (maxTime <= 0) return;

        float fill = currentTime / maxTime;
        fillImage.fillAmount = fill;
    }

    public void Hide()
    {
        gameObject.SetActive(false); 
    }
}
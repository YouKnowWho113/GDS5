using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 10f;

    Vector3 originalPos;
    float currentTime = 0f;
    public Pause pauseMenu;

    void Start()
    {
        originalPos = transform.localPosition; 
    }

    public void Shake()
    {
        currentTime = duration;
        transform.localPosition = originalPos; 
    }
    void Update()
    {
        

        if (Pause.GameIsPaused)
        {
            transform.localPosition = originalPos;
            return;
        }

        if (currentTime > 0)
        {
            float strength = currentTime / duration;

            float x = Random.Range(-1f, 1f) * magnitude * strength;
            float y = Random.Range(-1f, 1f) * magnitude * strength;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                transform.localPosition = originalPos; 
            }
        }
    }
    public void StopShake()
    {
        currentTime = 0f;
        transform.localPosition = originalPos;
    }
}
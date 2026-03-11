using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    public KeyCode key;          
    private Renderer rend;
    private Color firstColour;
    public AudioSource audioSource;

    void Start()
    {
        rend = GetComponent<Renderer>();
        firstColour = rend.material.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        rend.material.color = Color.red;
        audioSource.Play();

        yield return new WaitForSeconds(0.2f);

        rend.material.color = firstColour;
    }
}
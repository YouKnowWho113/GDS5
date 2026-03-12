using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    public KeyCode key;          
    private Renderer rend;
    private Color firstColour;
    public AudioSource audioSource;
    public Sprite[] rendererPool;
    public Sprite currentSprite;

    int currentIndex = 0;

    void Update()
    {
        currentSprite = rendererPool[currentIndex];

        if (Input.GetKey(key))
        {
           
            currentIndex = 1;
            Debug.Log("Pressed");
            
        }
        else currentIndex = 0;
        
        this.GetComponent<SpriteRenderer>().sprite = currentSprite;

        if (Input.GetKeyDown(key))
        {
            audioSource.Play();
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCheck : MonoBehaviour
{
    public KeyCode[] requiredKeys;   

    int currentIndex = 0;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode expectedKey = requiredKeys[currentIndex];

            
            if (Input.GetKeyDown(expectedKey))
            {
                Debug.Log("Correct: " + expectedKey);
                currentIndex++;

                CheckCompletion();
            }
            else
            {
                
                FailSequence();
            }
        }
    }

    void CheckCompletion()
    {
        if (currentIndex >= requiredKeys.Length)
        {
            Debug.Log("Completed!");
            Destroy(gameObject);
        }
    }

    void FailSequence()
    {
        Debug.Log("Wrong key!");
        currentIndex = 0;
    }
}
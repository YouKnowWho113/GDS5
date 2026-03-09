using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCheck : MonoBehaviour
{
    public KeyCode[] requiredKeys;   
    private HashSet<KeyCode> pressedKeys = new HashSet<KeyCode>();

    void Update()
    {
        if (Input.anyKeyDown)
        {
            bool correctKeyPressed = false;

            foreach (KeyCode key in requiredKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    correctKeyPressed = true;

                    if (!pressedKeys.Contains(key))
                    {
                        pressedKeys.Add(key);
                        Debug.Log("Correct: " + key);
                    }

                    CheckCompletion();
                }
            }

         
            if (!correctKeyPressed)
            {
                FailSequence();
            }
        }
    }

    void CheckCompletion()
    {
        if (pressedKeys.Count == requiredKeys.Length)
        {
            Debug.Log("Chord Completed!");
            Destroy(gameObject);
        }
    }

    void FailSequence()
    {
        Debug.Log("Wrong key! Resetting input.");
        pressedKeys.Clear();
    }
}
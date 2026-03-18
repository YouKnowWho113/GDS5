using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCheck : MonoBehaviour
{
    public KeyCode[] requiredKeys;
    public bool destroyed;
    bool completed = false;
    int currentIndex = 0;
    public EnemyManager manager;
    public NoteUIController ui;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode expectedKey = requiredKeys[currentIndex];

            if (Input.GetKeyDown(expectedKey))
            {
                Debug.Log("Correct: " + expectedKey);

                currentIndex++;
                ui.OnCorrectInput();

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
            manager.OnObstacleCleared();
            Debug.Log("Completed!");
            Destroy(gameObject);
        }
    }

    void FailSequence()
    {
        Debug.Log("Wrong key!");
        ui.OnFail();
        currentIndex = 0;
    }
}

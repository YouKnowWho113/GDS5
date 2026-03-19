using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPhaseEnemy : MonoBehaviour
{
    public KeyCode[] phase1Keys;
    public KeyCode[] phase2Keys;

    KeyCode[] currentKeys;

    int currentIndex = 0;
    int currentPhase = 1;

    public NewNoteUIController ui;

    [Header("Phase 2 Push")]
    public float pushDistance = 2f;
    public float pushSpeed = 5f;

    bool isPushing = false;
    Vector3 targetPosition;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public EnemyManager manager;

    void Start()
    {
        if (phase1Keys == null || phase1Keys.Length == 0)
        {
            Debug.LogError("Phase1Keys NOT SET!");
            return;
        }

        currentPhase = 1;
        currentKeys = phase1Keys;
        currentIndex = 0;

        ActivateNotes(currentKeys.Length);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode expectedKey = currentKeys[currentIndex];

            if (Input.GetKeyDown(expectedKey))
            {
                ui.HideNoteAtIndex(currentIndex);
                currentIndex++;

                CheckComplete();
            }
            else
            {
                
                currentIndex = 0;

                ui.OnFail();

                ActivateNotes(currentKeys.Length); 
            }
        }

        HandleMovement();
    }

    void HandleMovement()
    {
        if (isPushing)
        {
            float step = pushSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isPushing = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    void CheckComplete()
    {
        if (currentIndex >= currentKeys.Length)
        {
            if (currentPhase == 1)
            {
                StartPhase2();
            }
            else
            {
                manager.OnObstacleCleared();
                Destroy(gameObject);
            }
        }
    }

    void StartPhase2()
    {
        currentPhase = 2;
        currentKeys = phase2Keys;
        currentIndex = 0;

        ActivateNotes(currentKeys.Length);
        StartPushBack();
    }

    void ActivateNotes(int count)
    {
        if (count <= 0) return;

        ui.SetupNotes(currentKeys); 
    }

    void StartPushBack()
    {
        isPushing = true;
        targetPosition = transform.position + Vector3.left * pushDistance;
    }
}
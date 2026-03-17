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

        Debug.Log("Enemy started, Phase 1");

        ActivateNotes(currentKeys.Length);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            KeyCode expectedKey = currentKeys[currentIndex];
            Debug.Log("Key pressed");



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
            }
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
    }

    void ActivateNotes(int count)
    {
        if (count <= 0)
        {
            Debug.LogWarning("ActivateNotes called with 0 — ignored");
            return;
        }

        Debug.Log("ActivateNotes called. Count = " + count);

        ui.ResetUI(); 


        for (int i = 0; i < ui.noteImages.Length; i++)
        {
            if (i < count)
            {
                ui.noteImages[i].gameObject.SetActive(true);
                ui.noteImages[i].transform.localScale = new Vector3(2f, 5.5f, 1f);
                

                Sprite s = ui.GetSpriteFromKey(currentKeys[i]);

                ui.noteImages[i].sprite = s;
            }
            else
            {
                ui.noteImages[i].gameObject.SetActive(false);
            }
        }
    }
}

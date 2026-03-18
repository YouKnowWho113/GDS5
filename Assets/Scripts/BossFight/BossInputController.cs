using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInputController : MonoBehaviour
{
    public KeyCode[] currentSequence;

    int currentIndex = 0;

    [Header("Timing")]
    public float timeLimit = 3f;
    float timer;

    public BossController boss;
    public GameObject sonicWavePrefab;
    public Transform spawnPoint;

    public NewNoteUIController ui;

    void Start()
    {
        StartNewSequence();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Fail();
            return;
        }

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentSequence[currentIndex]))
            {
                ui.HideNoteAtIndex(currentIndex);
                currentIndex++;

                if (currentIndex >= currentSequence.Length)
                {
                    Success();
                }
            }
            else
            {
                Fail();
            }
        }
    }
    void Success()
    {
        Debug.Log("Correct!");

        SpawnWave();

        StartNewSequence();
    }

    void Fail()
    {
        Debug.Log("Failed!");

        boss.AttackPlayer();

        StartNewSequence();
    }

    void SpawnWave()
    {
        Instantiate(sonicWavePrefab, spawnPoint.position, Quaternion.identity);
    }

    void StartNewSequence()
    {
        currentIndex = 0;
        timer = timeLimit;

        GenerateRandomSequence();

        ui.ResetUI();
        ActivateUI();
    }
}
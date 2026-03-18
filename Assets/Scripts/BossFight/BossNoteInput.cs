using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNoteInput : MonoBehaviour
{
    KeyCode[] pattern;
    int index = 0;

    public BossController boss;
    public GameObject sonicWavePrefab;
    public Transform spawnPoint;

    bool completed = false;

    public void SetPattern(KeyCode[] newPattern)
    {
        pattern = newPattern;
        index = 0;
        completed = false;
    }

    void Update()
    {
        if (pattern == null || completed) return;

        if (Input.anyKeyDown)
        {
            KeyCode expected = pattern[index];

            if (Input.GetKeyDown(expected))
            {
                index++;

                if (index >= pattern.Length)
                {
                    completed = true;
                    SpawnWave();
                }
            }
            else
            {
                index = 0;
                boss.OnPlayerFail();
            }
        }
    }

    void SpawnWave()
    {
        Instantiate(sonicWavePrefab, spawnPoint.position, Quaternion.identity);
    }
}

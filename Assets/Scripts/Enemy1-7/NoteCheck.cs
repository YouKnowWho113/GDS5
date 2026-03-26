using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCheck : MonoBehaviour
{
    public KeyCode[] requiredKeys;
    public bool destroyed;
    //bool completed = false;
    int currentIndex = 0;
    public EnemyManager manager;
    public NoteUIController ui;

    public GameObject sonicWavePrefab;
    public Transform spawnPoint;
    public AudioSource hurtAudio;

    public SpriteRenderer sr;
    public Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    void Update()
    {
        if (Pause.GameIsPaused) return;
        if (Input.anyKeyDown && currentIndex < requiredKeys.Length)
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
            Debug.Log("Completed!");
            hurtAudio.Play();

            SpawnWave();
           
        }
    }
    

    void FailSequence()
    {
        Debug.Log("Wrong key!");
        ui.OnFail();
        currentIndex = 0;
    }
    void SpawnWave()
    {
        GameObject wave = Instantiate(sonicWavePrefab, spawnPoint.position, Quaternion.identity);

        SonicWave sw = wave.GetComponent<SonicWave>();
        sw.manager = manager; 
    }

    
    
    
}

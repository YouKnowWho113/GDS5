using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [Header("Boss HP")]
    public int maxHP = 7;
    public int currentHP;

    [System.Serializable]
    public class Phase
    {
        public KeyCode[] keys;
    }

    [Header("Phases")]
    public Phase[] phases;

    int currentPhase = 0;

    public BossNoteInput input;
    public BossUIController ui;
    public ShakeScreen screenShake;

    public EnemyMoving enemy;

    [Header("Timer")]
    public float phaseTimeLimit = 3f;
    float currentTimer;
    bool timerRunning = false;

    public PlayerHP player;
    public BossTimerUI timerUI;

    void Start()
    {
        currentHP = maxHP;
        StartPhase();
    }

    void StartPhase()
    {
        if (currentHP <= 0)
        {
            Debug.Log("Boss defeated");
            Destroy(gameObject);
            return;
        }

        if (currentPhase >= phases.Length)
        {
            Debug.Log("No more phases");
            return;
        }

        KeyCode[] pattern = phases[currentPhase].keys;

        input.SetPattern(pattern);
        ui.ShowNotes(pattern);


        currentTimer = phaseTimeLimit;
        timerRunning = true;
        timerUI.Init(phaseTimeLimit);

        Debug.Log("Start Phase: " + (currentPhase + 1));
    }

    public void OnWaveHit()
    {
        timerRunning = false;
        timerUI.Hide(); 

        TakeDamage(1);

        if (currentHP > 0)
        {
            currentPhase++;
            Invoke(nameof(StartPhase), 1f);
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        Debug.Log("Boss HP: " + currentHP);

        if (currentHP <= 0)
        {
            Debug.Log("Boss Dead");
            Destroy(gameObject);
        }
    }

    public void OnPlayerFail()
    {
        if (screenShake != null)
        {
            screenShake.Shake();
        }

        StartCoroutine(FailRoutine());
    }

    IEnumerator FailRoutine()
    {
        ui.OnFail();

        yield return new WaitForSeconds(0.2f);

        ui.ResetUI();

        
        KeyCode[] pattern = phases[currentPhase].keys;
        input.SetPattern(pattern);
        ui.ShowNotes(pattern);
    }

    void Update()
    {
        if (currentHP <= 0)
        {
            SceneManager.LoadScene("Main Menu");
        }

        if (timerRunning && enemy.playerInSight)
        {
            currentTimer -= Time.deltaTime;

            timerUI.UpdateBar(currentTimer);

            if (currentTimer <= 0)
            {
                timerRunning = false;
                OnTimeOut();
            }
        }
    }

    void OnTimeOut()
    {
        Debug.Log("Time Out!");

        //animation

        player.TakeDamage(1);

        StartCoroutine(TimeOutRoutine()); 
    }

    IEnumerator TimeOutRoutine()
    {
        timerRunning = false;
        timerUI.Hide();

        ui.OnFail();

        yield return new WaitForSeconds(0.2f);

        ui.ResetUI();

       
        StartPhase();
    }

    public void OnInputComplete()
    {
        timerRunning = false;
        timerUI.Hide();
    }
}
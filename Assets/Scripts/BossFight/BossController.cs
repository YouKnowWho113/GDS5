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
    public bool timerRunning = false;

    public PlayerHP player;
    public BossTimerUI timerUI;

    public Animator animator;

    public GameObject sonicWavePrefab;
    public Transform spawnPoint;
    bool isAttacking = false;

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
            SceneManager.LoadScene("Main Menu");
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

        yield return new WaitForSeconds(0.6f);

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

    IEnumerator AttackRoutine()
    {
        if (isAttacking) yield break;

        isAttacking = true;

        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(0.45f);

        Instantiate(sonicWavePrefab, spawnPoint.position, Quaternion.identity);
        

        animator.SetBool("isAttacking", false);

        isAttacking = false;
    }

    void OnTimeOut()
    {
        Debug.Log("Time Out!");

        StartCoroutine(HandleTimeOut()); 
    }

    IEnumerator HandleTimeOut()
    {
        timerRunning = false;
        timerUI.Hide();

        ui.OnFail();

        
        StartCoroutine(AttackRoutine());

        
        yield return new WaitForSeconds(0.1f); 
        ui.ResetUI();

        
        yield return new WaitForSeconds(1f); 

        StartPhase();
    }
    public void OnInputComplete()
    {
        timerRunning = false;
        timerUI.Hide();
    }
    public void SpawnWaveFromAnimation()
    {
        Debug.Log("Boss Spawn Wave!");

        Instantiate(sonicWavePrefab, spawnPoint.position, Quaternion.identity);
    }

}
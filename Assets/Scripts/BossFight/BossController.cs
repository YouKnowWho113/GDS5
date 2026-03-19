using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Debug.Log("Start Phase: " + (currentPhase + 1));
    }

    public void OnWaveHit()
    {
        TakeDamage(1);

        if (currentHP > 0)
        {
            currentPhase++;
            Invoke(nameof(StartPhase), 0.3f);
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
        StartPhase();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 5;
    public int currentHP;
    public int CurrentHP => currentHP;
    public ShakeScreen screenShake;
    public AudioSource hurtAudio;

    public float invincibleTime = 2f;
    bool isInvincible = false;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return; 

        currentHP -= amount;
        Debug.Log("Player HP: " + currentHP);

        if (screenShake != null)
        {
            screenShake.Shake();
            hurtAudio.Play();
        }
        StartCoroutine(InvincibleRoutine());

        if (currentHP <= 0)
        {
            Die();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Wave"))
        {
            TakeDamage(1);
        }
    }
    IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
    }


    void Die()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Player Dead!");
        
    }
}
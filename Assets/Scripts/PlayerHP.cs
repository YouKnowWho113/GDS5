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
    public SpriteRenderer sr;
    public Color originalColor;

    void Start()
    {
        currentHP = maxHP;

        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return; 

        currentHP -= amount;
        Debug.Log("Player HP: " + currentHP);
        StartCoroutine(FlashRed());

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
        SceneManager.LoadScene("Defeat");
        Debug.Log("Player Dead!");
        
    }

    IEnumerator FlashRed()
    {
        sr.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        sr.color = originalColor;
    }
}
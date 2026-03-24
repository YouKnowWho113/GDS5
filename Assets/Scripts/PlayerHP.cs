using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 5;
    public int currentHP;
<<<<<<< HEAD
    public int CurrentHP => currentHP;
=======
    public Texture heartDisplay;
    public Texture emptyHeart;
    public RawImage[] heart;
>>>>>>> db240ca2e3b586dca469b0ba03afd692167879f5

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        for (int i = 0; i < heart.Length; i++)
        {
            if (i < currentHP)
            {
                heart[i].texture = heartDisplay;
            }
            else
            {
                heart[i].texture = emptyHeart;
            }

            if (i < maxHP)
            {
                heart[i].enabled = true;
            }
            else
            {
                heart[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log("Player HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Dead!");
        
    }
}
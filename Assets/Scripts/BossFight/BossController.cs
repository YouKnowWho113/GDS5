using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("HP")]
    public int maxHP = 5;
    int currentHP;

    [Header("Attack")]
    public GameObject attackEffect;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;

        Debug.Log("Boss HP: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void AttackPlayer()
    {
        Debug.Log("Boss attacks!");

        if (attackEffect != null)
            Instantiate(attackEffect, transform.position, Quaternion.identity);
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }
}
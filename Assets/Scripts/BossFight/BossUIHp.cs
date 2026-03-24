using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIHp : MonoBehaviour
{
    public Image[] hpBars;
    public BossController boss;

    int maxHP;

    void Start()
    {
        maxHP = hpBars.Length;
        UpdateHP(boss.currentHP);
    }

    void Update()
    {
        UpdateHP(boss.currentHP);
    }

    void UpdateHP(int hp)
    {
        for (int i = 0; i < hpBars.Length; i++)
        {
           
            hpBars[i].enabled = i >= (maxHP - hp);
        }
    }
}

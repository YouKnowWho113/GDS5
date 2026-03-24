using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHearts : MonoBehaviour
{
    public Image[] hearts;
    public PlayerHP playerHP;

    int maxHP;

    void Start()
    {
        maxHP = hearts.Length;
        UpdateHearts(playerHP.CurrentHP);
    }

    void Update()
    {
        UpdateHearts(playerHP.CurrentHP);

       
    }

    void UpdateHearts(int hp)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            
            hearts[i].enabled = i >= (maxHP - hp);
        }
    }
}
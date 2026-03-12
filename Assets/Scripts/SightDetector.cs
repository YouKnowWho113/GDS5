using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightDetector : MonoBehaviour
{
    public PlayerMoving player;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            player.enemyInSight = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            player.enemyInSight = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightDetection : MonoBehaviour
{
    public EnemyMoving enemy;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.playerInSight = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.playerInSight = false;
        }
    }
}

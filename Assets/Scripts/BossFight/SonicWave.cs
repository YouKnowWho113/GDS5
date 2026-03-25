using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWave : MonoBehaviour
{
    public float speed = 10f;
    public EnemyManager manager;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit something: " + other.name);

        if (other.CompareTag("Boss"))
        {
            Debug.Log("HIT BOSS");

            BossController boss = other.GetComponent<BossController>();

            if (boss != null)
            {
                boss.OnWaveHit();
            }

            Destroy(gameObject);
        }

        else if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            manager.OnObstacleCleared();
        }
        else if (other.CompareTag("Enemy1"))
        {
            MultiPhaseEnemy enemy = other.GetComponent<MultiPhaseEnemy>();

            if (enemy != null)
            {
                enemy.OnWaveHit();
            }

            Destroy(gameObject);
        }

    }
}
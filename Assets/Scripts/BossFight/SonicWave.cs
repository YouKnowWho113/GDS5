using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWave : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            BossController boss = other.GetComponent<BossController>();

            if (boss != null)
            {
                boss.OnWaveHit();
            }

            Destroy(gameObject);
        }
    }
}
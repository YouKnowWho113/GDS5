using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWave : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BossController boss = other.GetComponent<BossController>();

        if (boss != null)
        {
            boss.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}

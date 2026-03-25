using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWaveBoss : MonoBehaviour
{
    public float speed = 10f;
    bool hasHit = false;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit) return;

        if (other.CompareTag("Player"))
        {
         

            Destroy(gameObject);
        }
    }
}
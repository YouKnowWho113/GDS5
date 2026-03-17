using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startPosition;
    }
}
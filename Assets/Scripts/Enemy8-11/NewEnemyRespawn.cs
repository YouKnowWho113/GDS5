using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyRespawn : MonoBehaviour
{
    private Vector3 startPosition;
    public NewNoteUIController ui;
    public MultiPhaseEnemy enemy;

    void Start()
    {
        startPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Respawn();
            enemy.ResetState(); 
        }
    }

    void Respawn()
    {
        transform.position = startPosition;
    }
}
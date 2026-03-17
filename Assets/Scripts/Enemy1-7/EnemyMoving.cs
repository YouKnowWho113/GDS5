using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public bool playerInSight = false;
    public float speed = -3f;


    void Update()
    {
        if (playerInSight == false)
        {
            speed = -3f;
            Move();
        }
        else
        {
            speed = 0f;
        }
    }
    void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

}

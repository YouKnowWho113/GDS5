using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed = 8f;
    public bool enemyInSight = false;

    

    void Update()
    {
        if (enemyInSight == false)
        {
            speed = 8f;
            MoveLeftRight();
        }
        else
        {
            speed = 0f;
        }
    }

    void MoveLeftRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
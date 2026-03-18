using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] obstacles;
    private int currentIndex = 0;

    void Start()
    {

        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }


        if (obstacles.Length > 0)
        {
            obstacles[0].SetActive(true);
        }
    }

    public void OnObstacleCleared()
    {
        currentIndex++;

        Debug.Log("Current Index: " + currentIndex);

        if (currentIndex < obstacles.Length)
        {
            obstacles[currentIndex].SetActive(true);
        }
        else
        {
            Debug.Log("cleared!");
        }
    }
}
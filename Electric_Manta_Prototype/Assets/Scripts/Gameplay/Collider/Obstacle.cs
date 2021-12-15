using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public bool isCar;
    public bool isCriminal;
    public bool obstacleTriggered = false;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isCar && other.tag == "Player" && obstacleTriggered == false)
        {
            gameManager.isPlayerDead = true;
            gameManager.diedByCar = true;
            gameManager.KillPlayer();
            Debug.Log("Player Hit");
            obstacleTriggered = true;
        }

        if (isCriminal && other.tag == "Player" && obstacleTriggered == false)
        {
            gameManager.isPlayerDead = true;
            gameManager.diedByCriminal = true;
            gameManager.KillPlayer();
            Debug.Log("Player Hit");
            obstacleTriggered = true;
        }

        else
        {
            return;
        }
    }
}

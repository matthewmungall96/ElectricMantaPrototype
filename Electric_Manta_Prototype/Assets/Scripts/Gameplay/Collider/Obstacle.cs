using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    void OnTriggerEnter(Collider other)
    {
        gameManager.isPlayerDead = true;
        Debug.Log("Player Hit");
    }
}

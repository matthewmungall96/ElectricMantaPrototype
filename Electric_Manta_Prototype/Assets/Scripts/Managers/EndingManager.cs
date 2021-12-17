using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        gameManager.EndGame();
    }
}

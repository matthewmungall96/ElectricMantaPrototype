using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool isCoin;
    public bool isCriminal;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCoin)
        {
            gameManager.Coins = gameManager.Coins + 1;
        }

        else if (isCriminal)
        {
            gameManager.CriminalsCaught = gameManager.CriminalsCaught + 1;
        }    
    }
}

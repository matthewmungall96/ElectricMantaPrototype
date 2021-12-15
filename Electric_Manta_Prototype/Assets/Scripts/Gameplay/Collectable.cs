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

    private void Update()
    {
        transform.Rotate(1, 0, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isCoin && other.tag == "Player")
        {
            gameManager.Coins = gameManager.Coins + 1;
            Destroy(gameObject);
        }

        else if (isCriminal && other.tag == "Player")
        {
            gameManager.CriminalsCaught = gameManager.CriminalsCaught + 1;
        }
    }
}

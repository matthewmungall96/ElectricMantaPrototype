using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    #region Variables
    public bool isCar;
    public bool isCriminal;
    public bool obstacleTriggered = false;
    private GameManager gameManager;
    #endregion

    #region Functions
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    void OnTriggerEnter(Collider other)
    {
        //makes sure that the player object is triggering the collider
        //as some triggers are close to each other, it could be accidently triggered by another trigger
        //checks to see if the player has been hit by either a car or a criminal
        if (other.tag == "Player" && obstacleTriggered == false)
        {
            gameManager.isPlayerDead = true;
            gameManager.diedByCar = true;
            gameManager.KillPlayer();

            if(isCar)
            {
                Debug.Log("Player Hit By Car");
            }            
            
            if(isCriminal)
            {
                Debug.Log("Player Hit By Criminal");
            }

            obstacleTriggered = true;
        }
    }
    #endregion
}

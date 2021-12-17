using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    #region Variables
    private GameManager gameManager;
    #endregion

    #region Functions
    private void Awake()
    {
        gameManager = GameManager.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    //used to trigger the ending components
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.EndGame();
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Variables
    public Animator enemyAnimator;
    public bool attackingEnemy;
    public bool runningEnemy;
    public bool enemyFleeing;
    public bool enemyIsDead;
    public float enemySpeed = 1f;
    public GameObject fistAttack;
    private GameManager gameManager;
    #endregion

    #region Functions
    private void Awake()
    {
        gameManager = GameManager.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    private void Update()
    {
        //if enemy is fleeing, they will be running away from the player
        //this is done by moving the enemy at a predefined speed
        if (gameManager.isPlayerDead == false && gameManager.gameStarted == true && enemyFleeing == true && enemyIsDead == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed, Space.World);
        }

        //disables the attack collider from the enemy model's fist
        if (enemyFleeing == true)
        {
            fistAttack.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameManager.isPlayerDead == false)
        {
            //checks to see if the enemy is enabled to attack the player
            if (attackingEnemy)
            {
                if (gameManager.isPlayerDead == false)
                {
                    //if player is not dead, it will enable the bool for attacking the player
                    enemyAnimator.SetBool("PlayerClose", true);
                }

                else
                {
                    //if player is dead, it will remove the bool for attacking the player
                    enemyAnimator.SetBool("PlayerClose", false);
                }
            }

            //checks to see if the enemy is enabled to attack the player
            if (runningEnemy)
            {
                if (gameManager.isPlayerDead == false)
                {
                    //if player is not dead, it will enable the bool for fleeing from the player
                    enemyFleeing = true;
                    enemyAnimator.SetBool("TurningToFlee", true);
                }

                else
                {
                    return;
                }
            }
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator enemyAnimator;
    public bool attackingEnemy;
    public bool runningEnemy;
    public bool enemyFleeing;
    public bool enemyIsDead;
    public float enemySpeed = 1f;
    public GameObject fistAttack;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    private void Update()
    {
        if (gameManager.isPlayerDead == false && gameManager.gameStarted == true && enemyFleeing == true && enemyIsDead == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * enemySpeed, Space.World);
        }

        if (enemyFleeing == true)
        {
            fistAttack.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameManager.isPlayerDead == false)
        {
            if (attackingEnemy)
            {
                if (gameManager.isPlayerDead == false)
                {
                    enemyAnimator.SetBool("PlayerClose", true);
                }

                else
                {
                    enemyAnimator.SetBool("PlayerClose", false);
                }
            }

            if (runningEnemy)
            {
                if (gameManager.isPlayerDead == false)
                {
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
}

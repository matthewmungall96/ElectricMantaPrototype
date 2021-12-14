using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("Player Movement Options")]
    public float playerSpeed = 1f;
    public float playerDirectionSpeed = 1f;

    [Header("Player State")]
    public bool playerIsAtLeftBoundary = false;
    public bool playerIsAtRightBoundary = false;

    [Header("External References")]
    public GameManager gameManager;
    public LevelSettings levelSettings;

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlayerDead == false && gameManager.gameStarted == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.gameObject.transform.position.x > levelSettings.LeftSideLimit)
                {
                    playerIsAtRightBoundary = false;
                    transform.Translate(Vector3.left * Time.deltaTime * playerDirectionSpeed);
                }

                else
                {
                    playerIsAtLeftBoundary = true;
                    return;
                }
            }            
            
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (this.gameObject.transform.position.x < levelSettings.RightSideLimit)
                {
                    playerIsAtLeftBoundary = false;
                    transform.Translate(Vector3.right * Time.deltaTime * playerDirectionSpeed);
                }

                else
                {
                    playerIsAtRightBoundary = true;
                    return;
                }

            }
        }
   
        else
        {
            return;
        }

    }

}

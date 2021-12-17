using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Player Movement Options")]
    public float playerSpeed = 1f;
    public float playerDirectionSpeed = 1f;

    [Header("Movement Bools")]
    public bool leftOn;
    public bool rightOn;

    [Header("Player State")]
    public bool movingRight = false;
    public bool movingLeft = false;
    public bool playerIsAtLeftBoundary = false;
    public bool playerIsAtRightBoundary = false;

    [Header("Debug Options")]
    public bool onMobile = false;
    public bool onComputer = false;

    [Header("External References")]
    private GameManager gameManager;
    private LevelSettings levelSettings;
    #endregion

    private void Awake()
    {
        #region External References
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        levelSettings = GameObject.FindObjectOfType(typeof(LevelSettings)) as LevelSettings;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Player World Navigation
        //if the game has started and the player is not dead, the player will move through the world space at the desired speed.
        if (gameManager.isPlayerDead == false && gameManager.gameStarted == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);
        }
        #endregion
        #region Mobile Controls
        //code for enabling mobile movement using touch control
        //player is able to move left by touching the left side, and right by touching the right side
        //will also detect if the player has reached the boundary of either side of the level (left or right)
        //will not move them if they have reached said boundary
        if (Input.touchCount > 0 && onMobile)
        {
            var touch = Input.GetTouch(0);

            //left side code
            if (touch.position.x < Screen.width / 2)
            {
                Debug.Log("Left click");
                if (gameManager.isPlayerDead == false && gameManager.gameStarted)
                {
                    //detects from level settings if the player has reached the furthest point of the left side
                    Debug.Log("Input Detected");
                    if (this.gameObject.transform.position.x > levelSettings.LeftSideLimit)
                    {
                        //will move further to the left side only if the player hasn't reached the boundary
                        Debug.Log("Moving Left");
                        playerIsAtRightBoundary = false;
                        transform.Translate(Vector3.left * Time.deltaTime * playerDirectionSpeed);
                    }

                    else
                    {
                        //player will not move at all if they are at the boundary
                        playerIsAtLeftBoundary = true;
                        return;
                    }
                }
            }

            //right side code
            else if (touch.position.x > Screen.width / 2)
            {
                Debug.Log("Right click");
                if (gameManager.isPlayerDead == false && gameManager.gameStarted)
                {
                    //detects from level settings if the player has reached the furthest point of the right side
                    Debug.Log("Input Detected");
                    if (this.gameObject.transform.position.x < levelSettings.RightSideLimit)
                    {
                        //will move further to the right side only if the player hasn't reached the boundary
                        Debug.Log("Moving Left");
                        playerIsAtLeftBoundary = false;
                        transform.Translate(Vector3.right * Time.deltaTime * playerDirectionSpeed);
                    }

                    else
                    {
                        //player will not move at all if they are at the boundary
                        playerIsAtRightBoundary = true;
                        return;
                    }
                }
            }
        }
        #endregion

        #region Computer Controls
        if (onComputer)
        {
            //left side code
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //detects from level settings if the player has reached the furthest point of the left side
                if (this.gameObject.transform.position.x > levelSettings.LeftSideLimit)
                {
                    //will move further to the left side only if the player hasn't reached the boundary
                    playerIsAtRightBoundary = false;
                    transform.Translate(Vector3.left * Time.deltaTime * playerDirectionSpeed);
                }

                else
                {
                    //player will not move at all if they are at the boundary
                    playerIsAtLeftBoundary = true;
                    return;
                }
            }

            //right side code
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //detects from level settings if the player has reached the furthest point of the right side
                if (this.gameObject.transform.position.x < levelSettings.RightSideLimit)
                {
                    //will move further to the right side only if the player hasn't reached the boundary
                    playerIsAtLeftBoundary = false;
                    transform.Translate(Vector3.right * Time.deltaTime * playerDirectionSpeed);
                }

                else
                {
                    //player will not move at all if they are at the boundary
                    playerIsAtRightBoundary = true;
                    return;
                }

            }
        }
   
        else
        {
            return;
        }
        #endregion
    }
}

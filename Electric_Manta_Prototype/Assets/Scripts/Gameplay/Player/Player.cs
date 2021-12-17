using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
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

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        levelSettings = GameObject.FindObjectOfType(typeof(LevelSettings)) as LevelSettings;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlayerDead == false && gameManager.gameStarted == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);
        }

        #region Mobile Controls
        if (Input.touchCount > 0 && onMobile)
        {
            var touch = Input.GetTouch(0);

            if (touch.position.x < Screen.width / 2)
            {
                Debug.Log("Left click");
                if (gameManager.isPlayerDead == false && gameManager.gameStarted)
                {
                    Debug.Log("Input Detected");
                    if (this.gameObject.transform.position.x > levelSettings.LeftSideLimit)
                    {
                        Debug.Log("Moving Left");
                        playerIsAtRightBoundary = false;
                        transform.Translate(Vector3.left * Time.deltaTime * playerDirectionSpeed);
                    }

                    else
                    {
                        playerIsAtLeftBoundary = true;
                        return;
                    }
                }
            }

            else if (touch.position.x > Screen.width / 2)
            {
                Debug.Log("Right click");
                if (gameManager.isPlayerDead == false && gameManager.gameStarted)
                {
                    Debug.Log("Input Detected");
                    if (this.gameObject.transform.position.x < levelSettings.RightSideLimit)
                    {
                        Debug.Log("Moving Left");
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
        }
        #endregion
        #region Computer Controls
        if (onComputer)
        {
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
        #endregion
    }
}

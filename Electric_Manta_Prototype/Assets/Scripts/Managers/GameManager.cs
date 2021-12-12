using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject menuCamera;

    [Header("Gameplay States")]
    public bool gameStarted = false;

    [Header("Game UI Elements")]
    public GameObject MainMenuUI;
    public GameObject characterDeathUI;
    public GameObject levelEndUI;

    [Header("Character Animator")]
    public Animator MainCharacterAnim;


    public void StartGame()
    {
        menuCamera.SetActive(false);
        mainCamera.SetActive(true);
        gameStarted = true;
        MainMenuUI.SetActive(false);
        MainCharacterAnim.SetTrigger("GameStarted");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Saveable Fields")]
    public int Coins;
    public int CriminalsCaught;
    public int totalCoins;
    public int bestCriminalsCaught;

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

    [Header("Text")]
    public TMP_Text totalCoinsText;

    void Start()
    {
        LoadFile();
    }

    void Update()
    {
        if (CriminalsCaught > bestCriminalsCaught)
        {
            bestCriminalsCaught = CriminalsCaught;
        }

        totalCoinsText.text = totalCoins.ToString();

    }

    public void StartGame()
    {
        menuCamera.SetActive(false);
        mainCamera.SetActive(true);
        gameStarted = true;
        MainMenuUI.SetActive(false);
        MainCharacterAnim.SetTrigger("GameStarted");
    }

    public void SaveFile()
    {
        SaveManager.SaveFile(this);
    }

    public void LoadFile()
    {
        SaveFile data = SaveManager.LoadPlayerFile(this);
        totalCoins = data.Coins;
        bestCriminalsCaught = data.Criminals;
    }
}

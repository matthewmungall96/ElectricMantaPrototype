using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool isPlayerJumping = false;

    [Header("Player Death States")]
    public bool isPlayerDead = false;
    public bool diedByCriminal = false;
    public bool diedByCar = false;

    [Header("Game UI Elements")]
    public GameObject MainMenuUI;
    public GameObject GameplayUI;
    public GameObject deathUI;
    public GameObject levelEndUI;

    [Header("Character Animator")]
    public Animator MainCharacterAnim;

    [Header("Enemy Character")]
    public Animator[] enemyAnimator;

    [Header("Text")]
    public TMP_Text totalCoinsText;
    public TMP_Text coinsCollectedText;
    public TMP_Text criminalsText;

    private void Awake()
    {

    }

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
        GameplayUI.SetActive(true);
        MainCharacterAnim.SetTrigger("GameStarted");
    }

    public void KillPlayer()
    {
        isPlayerDead = true;
        MainCharacterAnim.SetTrigger("HasBeenHit");

        for (int i = 0; i < enemyAnimator.Length; i++)
        {
            enemyAnimator[i].SetBool("PlayerHit", true);
        }

        coinsCollectedText.text = "You have gained " + Coins.ToString() + " Coins";
        criminalsText.text = "You have arrested " + CriminalsCaught.ToString() + " Criminals";
        Debug.Log("Player has been hit");
        StartCoroutine(DeathDelay());
    }

    IEnumerator DeathDelay()
    {
        totalCoins = totalCoins + Coins;
        SaveFile();
        yield return new WaitForSeconds(0.5f);
        deathUI.SetActive(true);
    }

    public void EndGame()
    {

    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Main_Menu");
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

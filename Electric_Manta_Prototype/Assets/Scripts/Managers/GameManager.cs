using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Character")]
    public GameObject playerCharacter;

    [Header("Player Costumes")]
    public GameObject[] playerCostumes;
    public int costumeSelected = 0;

    [Header("Saveable Fields")]
    public int coins;
    public int criminalsCaught;
    public int totalCoins;
    public int bestCriminalsCaught;

    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject menuCamera;

    [Header("Gameplay States")]
    public bool gameStarted = false;
    public bool isPlayerJumping = false;
    public bool gameEnded = false;

    [Header("Finish Goal")]
    public GameObject finishingLine;
    public Slider distanceSlider;
    public float totalDistance;
    public float percentageOfLevelComplete;

    [Header("Distance Variable")]
    public float distanceBetweenGoals;

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
    public List<GameObject> enemyObject;

    [Header("Text")]
    public TMP_Text[] totalCoinsText;
    public TMP_Text coinsText;
    public TMP_Text[] coinsCollectedText;
    public TMP_Text[] criminalsText;
    public TMP_Text calculationText;

    private void Awake()
    {
        LoadFile();
        enemyObject = new List<GameObject>();
        enemyObject.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        coinsText.text = coins.ToString();
        if (criminalsCaught > bestCriminalsCaught)
        {
            bestCriminalsCaught = criminalsCaught;
        }

        for (int i = 0; i < totalCoinsText.Length; i++)
        {
            totalCoinsText[i].text = totalCoins.ToString();
        }

        for (int i = 0; i < coinsCollectedText.Length; i++)
        {
            if (coins == 0)
            {
                coinsCollectedText[i].text = "You have gained O Coins";
            }

            else
            {
                coinsCollectedText[i].text = "You have gained " + coins.ToString() + " Coins";
            }
        }

        for (int i = 0; i < criminalsText.Length; i++)
        {
            if (criminalsCaught == 0)
            {
                criminalsText[i].text = "You have arrested O Criminals";
            }

            else
            {
                criminalsText[i].text = "You have arrested " + criminalsCaught.ToString() + " Criminals";
            }
        }

        for (int i = 0; i < totalCoinsText.Length; i++)
        {
            totalCoinsText[i].text = totalCoins.ToString();
        }


        distanceBetweenGoals = Vector3.Distance(playerCharacter.transform.position, finishingLine.transform.position);
        distanceSlider.value = Mathf.InverseLerp(distanceBetweenGoals, 0f, totalDistance / 1);
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
        costumeSelected = data.Costume;
    }

    public void StartGame()
    {
        menuCamera.SetActive(false);
        mainCamera.SetActive(true);
        gameStarted = true;
        MainMenuUI.SetActive(false);
        GameplayUI.SetActive(true);
        MainCharacterAnim.SetTrigger("GameStarted");
        totalDistance = distanceBetweenGoals;
    }

    public void KillPlayer()
    {
        isPlayerDead = true;
        MainCharacterAnim.SetTrigger("HasBeenHit");

        foreach(GameObject enemy in enemyObject)
        {
            Animator enemyDance = enemy.GetComponent<Animator>();
            enemyDance.SetTrigger("PlayerHit");
        }

        Debug.Log("Player has been hit");
        StartCoroutine(DeathDelay());
    }

    public void CollectedCriminal()
    {
        enemyObject = new List<GameObject>();
        enemyObject.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.5f);
        deathUI.SetActive(true);
        SaveFile();
    }


    public void ResetGame()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void EndGame()
    {

    }


}

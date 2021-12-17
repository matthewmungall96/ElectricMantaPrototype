using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Player Character")]
    public GameObject playerCharacter;

    [Header("Player Costumes")]
    public GameObject[] playerCostumes;
    public int costumeSelected;

    [Header("Saveable Fields")]
    public int coins;
    public int criminalsCaught;
    public double totalCoins;
    public int bestCriminalsCaught;

    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject menuCamera;
    public GameObject endingCamera;

    [Header("Gameplay States")]
    public bool gameStarted = false;
    public bool gamePaused = false;
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
    public GameObject mainMenuUI;
    public GameObject gameplayUI;
    public GameObject PauseUI;
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
    public TMP_Text endingSubHeading;
    public TMP_Text calculationText;
    #endregion

    private void Awake()
    {
        LoadFile();
        enemyObject = new List<GameObject>();
        enemyObject.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        coinsText.text = coins.ToString();

        //Time.timeScale is how quickly time will move during play
        //the gamePaused bool is controlled via the PauseGame() function
        if (gamePaused)
        {
            Time.timeScale = 0f;
        }

        else
        {
            Time.timeScale = 1f;
        }

        //compares the current criminals caught int with the recorded highest nunber of criminals caught in a succession
        //if the current is higher than the previous best, it will be recorded in the save file
        if (criminalsCaught > bestCriminalsCaught)
        {
            bestCriminalsCaught = criminalsCaught;
        }

        //records the same output for each TMP_Text field in the array
        //currently, outputs the total number of coins the player has to string
        for (int i = 0; i < totalCoinsText.Length; i++)
        {
            totalCoinsText[i].text = totalCoins.ToString();
        }

        //records the same output for each TMP_Text field in the array
        //currently, outputs the total number of coins collected during their run of the level to string
        for (int i = 0; i < coinsCollectedText.Length; i++)
        {
            coinsCollectedText[i].text = "You have gained " + coins.ToString() + " Coins";
        }

        //records the same output for each TMP_Text field in the array
        //currently, outputs the total number of criminals collected during their run of the level to string
        for (int i = 0; i < criminalsText.Length; i++)
        {
            criminalsText[i].text = "You have arrested " + criminalsCaught.ToString() + " Criminals";
        }

        distanceBetweenGoals = Vector3.Distance(playerCharacter.transform.position, finishingLine.transform.position);
        distanceSlider.value = Mathf.InverseLerp(distanceBetweenGoals, 0f, totalDistance / 1);
    }

    public void SaveFile()
    {
        //saves called variables to a file
        SaveManager.SaveFile(this);
    }

    public void LoadFile()
    {
        //loading the save file via the data declared in the SaveFile script
        SaveFile data = SaveManager.LoadPlayerFile(this);
        totalCoins = data.Coins;
        bestCriminalsCaught = data.Criminals;
        costumeSelected = data.Costume;
    }

    public void RandomCostume()
    {
        //int costumeChosen will be a random number each time the button is pressed
        //we choose this number by having 0 as our minimum number, and the number of costumes as the highest number
        int costumeChosen = Random.Range(0, playerCostumes.Length);

        for (int i = 0; i < playerCostumes.Length; i++)
        {
            //if i is equal to the random number generated in the costumeChosen int
            if(i == costumeChosen)
            {
                //that costume will be set to active
                playerCostumes[costumeChosen].SetActive(true);
            }

            else
            {
                //while the costumes that weren't chosen will not be active
                playerCostumes[i].SetActive(false);
            }
        }

        //logs the random number each time the button is pressed
        Debug.Log("Random Number was " + costumeChosen);
    }

    public void StartGame()
    {
        menuCamera.SetActive(false);
        mainCamera.SetActive(true);
        gameStarted = true;
        mainMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        MainCharacterAnim.SetTrigger("GameStarted");
        totalDistance = distanceBetweenGoals;
    }

    public void PauseGame()
    {
        //simple pausing mechanic
        //if the pause button is pressed, the game will pause
        //upon pressing the play button, it will unpause the game

        if (gamePaused == false)
        {
            gamePaused = true;
        }

        else
        {
            gamePaused = false;
        }
    }

    public void KillPlayer()
    {
        //confirms to other scripts using isPlayerDead that the player has been hit
        isPlayerDead = true;

        //plays the "Has Been Hit" animation for visual feedback
        MainCharacterAnim.SetTrigger("HasBeenHit");

        //each enemy will now be doing a celebratory dance due to the player being knocked down
        foreach(GameObject enemy in enemyObject)
        {
            Animator enemyDance = enemy.GetComponent<Animator>();
            enemyDance.SetTrigger("PlayerHit");
        }

        //records that player has been hit 
        Debug.Log("Player has been hit");

        //starts process of diplaying the Death UI component
        StartCoroutine(DeathDelay());
    }

    public void CollectedCriminal()
    {
        //generates a new list of enemies after one has been collected
        //it does this to avoid breaking the dancing in the KillPlayer() function
        enemyObject = new List<GameObject>();
        enemyObject.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(0.5f);
        deathUI.SetActive(true);
    }


    public void ResetGame()
    {
        //saves the game, and then reloads the current scene
        SaveFile();
        SceneManager.LoadScene("Main_Scene");
    }

    public void EndGame()
    {
        gameEnded = true;
        gameStarted = false;
        gameplayUI.SetActive(false);
        levelEndUI.SetActive(true);
        mainCamera.SetActive(false);
        endingCamera.SetActive(true);
        MainCharacterAnim.SetTrigger("GameOverDance");
        double criminalCoinCalc = criminalsCaught * 0.30;
        double coinsCalc = coins * criminalCoinCalc;

        if(criminalsCaught < 3)
        {
            endingSubHeading.text = "You didn't catch enough criminals!";
            calculationText.text = "";
        }

        else
        {
            endingSubHeading.text = "You caught enough criminals, good job!";
            calculationText.text = "Survival Bonus is " + coinsCalc.ToString() + " Coins";
        }

        totalCoins = totalCoins + coins + coinsCalc;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    #region Variables
    public double Coins;
    public int Criminals;
    public int Costume;
    #endregion

    #region Functions
    public SaveFile(GameManager gm)
    {
        //takes variables from the GameManager script
        //changes values of savefile variables to variables from GameManager
        Coins = gm.totalCoins;
        Criminals = gm.bestCriminalsCaught;
        Costume = gm.costumeSelected;
    }
    #endregion
}



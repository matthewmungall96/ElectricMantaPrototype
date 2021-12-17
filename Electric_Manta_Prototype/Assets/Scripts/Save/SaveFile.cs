using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public double Coins;
    public int Criminals;
    public int Costume;

    public SaveFile(GameManager gm)
    {
        Coins = gm.totalCoins;
        Criminals = gm.bestCriminalsCaught;
        Costume = gm.costumeSelected;
    }

}



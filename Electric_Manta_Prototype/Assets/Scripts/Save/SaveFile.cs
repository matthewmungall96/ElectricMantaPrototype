using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveFile
{
    public int Coins;
    public int Criminals;

    public SaveFile(GameManager gm)
    {
        Coins = gm.totalCoins;
        Criminals = gm.bestCriminalsCaught;
    }

}



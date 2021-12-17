using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Functions
    private void Awake()
    {
        //creates array of Music Players
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicPlayer");

        //removes any past the first one generated
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        //makes sure that the music player will persist through reloading the game
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion
}

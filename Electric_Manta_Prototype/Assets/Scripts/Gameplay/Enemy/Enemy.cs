using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public Vector3 range;
    public float speed;
    public Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == range)
        {
            Debug.Log("Player is at position: " + player.transform.position.ToString());
        }

        else
        {
            return;
        }
    }
}

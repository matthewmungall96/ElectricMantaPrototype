using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float range = 1f;
    public float speed;
    public Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) == range)
        {
            enemyAnimator.SetTrigger("PlayerClose");
        }

        else
        {
            return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    #region Variables
    public GameObject collectionContainer;
    public bool isCoin;
    public bool isCriminal;
    public GameObject collectionEffect;
    private SkinnedMeshRenderer objectRendererCriminal;
    private MeshRenderer objectRendererCoin;
    private GameManager gameManager;
    public AudioSource collectableGained;
    public Enemy enemyConfig;
    #endregion

    #region Functions
    private void Awake()
    {
        //detects if the collectable script needs to use either a mesh renderer or a skinned mesh renderer
        //this is due to both types of collectable using two different rendering meshs
        if (isCoin)
        {
            objectRendererCoin = gameObject.GetComponent<MeshRenderer>();
        }

        else
        {
            objectRendererCriminal = gameObject.GetComponentInParent<SkinnedMeshRenderer>();
        }

        gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    private void Update()
    {
        //if it is a coin, then it will rotate on the x-axis to give the iconic "spinning coin" look
        if (isCoin)
        {
            transform.Rotate(1, 0, 0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //ontrigger will be different depending on which boolean has been set
        //if the collectable is a coin, it will be added to the number of coins the player has gained in the level with the mesh renderer disabled
        //if the collectable is a criminal, it will be added to the number of criminals the player has arrested and the skinned mesh renderer is disabled
        //finally, it will begin the process of destroying the gameobject entirely
        if (isCoin && other.tag == "Player")
        {
            collectableGained.Play();
            gameManager.coins = gameManager.coins + 1;
            collectionEffect.SetActive(true);
            objectRendererCoin.enabled = !objectRendererCoin.enabled;
            StartCoroutine(DestroyObject());
        }

        else if (isCriminal && other.tag == "Player")
        {
            enemyConfig.enemyIsDead = true;
            collectableGained.Play();
            gameManager.enemyObject.Remove(this.gameObject);
            gameManager.criminalsCaught = gameManager.criminalsCaught + 1;
            collectionEffect.SetActive(true);
            objectRendererCriminal.enabled = !objectRendererCriminal.enabled;
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        //removes the game object from the scene
        //refreshes the criminal list from the game manager to ensure the dancing animation won't break
        collectionContainer.tag = "Collected";
        yield return new WaitForSeconds(0.2f);
        gameManager.CollectedCriminal();
        yield return new WaitForSeconds(2f);
        Destroy(collectionEffect);
        Destroy(collectionContainer);
    }
    #endregion
}

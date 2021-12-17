using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject collectionContainer;
    public bool isCoin;
    public bool isCriminal;
    public GameObject collectionEffect;
    private SkinnedMeshRenderer objectRendererCriminal;
    private MeshRenderer objectRendererCoin;
    private GameManager gameManager;
    public AudioSource collectableGained;
    public Enemy enemyConfig;

    private void Awake()
    {
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
        transform.Rotate(1, 0, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isCoin && other.tag == "Player")
        {
            collectableGained.Play();
            gameManager.coins = gameManager.coins + 1;
            gameManager.totalCoins = gameManager.totalCoins + 1;
            collectionEffect.SetActive(true);
            objectRendererCoin.enabled = !objectRendererCoin.enabled;
            StartCoroutine(DestroyParticleSystem());
        }

        else if (isCriminal && other.tag == "Player")
        {
            enemyConfig.enemyIsDead = true;
            collectableGained.Play();
            gameManager.enemyObject.Remove(this.gameObject);
            gameManager.criminalsCaught = gameManager.criminalsCaught + 1;
            collectionEffect.SetActive(true);
            objectRendererCriminal.enabled = !objectRendererCriminal.enabled;
            StartCoroutine(DestroyParticleSystem());
        }
    }

    IEnumerator DestroyParticleSystem()
    {
        collectionContainer.tag = "Collected";
        yield return new WaitForSeconds(0.2f);
        gameManager.CollectedCriminal();
        yield return new WaitForSeconds(2f);
        Destroy(collectionEffect);
        Destroy(collectionContainer);
    }
}

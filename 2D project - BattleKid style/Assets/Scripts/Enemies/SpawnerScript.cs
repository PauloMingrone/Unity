using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    SpriteRenderer selfSpriteRenderer;
    public GameObject enemyPrefab;
    Transform spawnLocation;
    

    // Start is called before the first frame update
    void Start()
    {
        //Joga sprite para ultimo sorting layer, tornando-se invisivel aos olhos do jogador
        selfSpriteRenderer = GetComponent<SpriteRenderer>();
        selfSpriteRenderer.sortingLayerName = "Spawners";
    
        spawnLocation = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnBecameVisible()
    {
        Instantiate(enemyPrefab, spawnLocation);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float bulletSpeed = 10f;
    GameObject player;
    PlayerMovement playerScript;
    bool destroyedByEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //posição horizontal (x) sempre mudando conforme velocidade do bullet e na direção que for criada (scaleX)
        transform.position += new Vector3 (bulletSpeed * Time.deltaTime * transform.localScale.x, 0f, 0f);
    }

    void OnBecameInvisible()
    {
        if (!destroyedByEnemy)
        {
            DestroyBullet();
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            AudioManager.instance.PlaySFX(3);
            destroyedByEnemy = true;
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        playerScript.IncreaseAmmo();
        Destroy(gameObject);
    }

    
}

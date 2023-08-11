using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatVScript : MonoBehaviour
{
    float enemySpeed = 3;
    int maxHP = 5;
    int currentHP;
    Transform enemyTransform;
    
    //Para zerar a gravidade desse objeto no Start() e possibilitar o movimento vertical smooth
    Rigidbody2D enemyRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        enemyTransform = GetComponent<Transform>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemyRigidbody2D.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //posição horizontal (x) sempre mudando conforme velocidade e na direção que for criada (scaleX)
        transform.position += new Vector3(0f, enemySpeed * Time.deltaTime, 0f);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    

    void DoDamage()
    {
        currentHP--;

        if (currentHP == 0)
        {
            AudioManager.instance.PlaySFX(2);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            DoDamage();
        }


        //se colisão com parede virar e andar para lado oposto
        if (collision.gameObject.tag == "FloorWall")
        {
          
            enemySpeed *= -1;
        }
    }


}

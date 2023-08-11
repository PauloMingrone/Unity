using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    float enemySpeed = -1;
    int maxHP = 3;
    int currentHP;
    Transform enemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        enemyTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //posição horizontal (x) sempre mudando conforme velocidade e na direção que for criada (scaleX)
        transform.position += new Vector3(enemySpeed * Time.deltaTime, 0f, 0f);
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
            if (enemySpeed < 0) {
                enemyTransform.localScale = new Vector3(-1, 1, 1);
            } else if (enemySpeed > 0)
            {
                enemyTransform.localScale = new Vector3(1, 1, 1);
            }
            enemySpeed *= -1;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementOriginalBackup : MonoBehaviour
{
    //Horizontal movement variables
    public float horizontalSpeed = 5;
    Rigidbody2D playerRB;
    SpriteRenderer playerSR; //Sprite adjustments

    //Jump variables 
    public float jumpSpeed = 10f;
    private bool isGrounded;
    public LayerMask groundLayer; // ground layer here
    public Transform groundCollider; // collider child object here

    //Shoot logic
    public GameObject firePrefab;
    Transform playerTransform;
    Vector3 firePos;
    Quaternion fireRotation;
    int maxAmmo = 3;
    int ammo;

    //for sprite animations
    Animator playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();

        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isGamePaused)
        {
            Move();

            Jump();

            //shoot logic
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
            
        }        
    }
    //Check when colision with objects tagged Enemy
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
            
        }
    }
    void Shoot()
    {
        if (ammo > 0) {
            ammo--;
            AudioManager.instance.PlaySFX(5);
            playerAnim.SetBool("isShooting", true);
            firePos = playerTransform.position;
            fireRotation = playerTransform.rotation;
            var bullet = Instantiate(firePrefab, firePos, fireRotation);
            if (playerSR.flipX)
            {
                bullet.transform.localScale = new Vector3(-1,1,1);
            }
            Invoke("StopShootingAnim", 1f);
        }

    }

    void StopShootingAnim()
    {
        playerAnim.SetBool("isShooting", false);
    }

    void Die()
    {
        GameMaster.instance.increaseDeath();
        //Animation event will call RespawnPlayer
        playerAnim.SetBool("isDead", true);
        AudioManager.instance.PlaySFX(2);
    }

    void RespawnPlayer()
    {
        playerAnim.SetBool("isDead", false);
        transform.position = GameMaster.instance.ReturnPlayerPosition();
        Camera.main.transform.position = GameMaster.instance.ReturnCameraPosition();
        GameMaster.instance.MapRespawn();
    }

    void Move()
    {
        //Player RB veolicity becomes horizontal speed * 1 or -1 is Horizontal input Happens (Input manager)
        playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * horizontalSpeed, playerRB.velocity.y);

        if (playerRB.velocity.x < 0)
        {
            playerSR.flipX = true;
        }
        else if (playerRB.velocity.x > 0)
        {
            playerSR.flipX = false;
        }
        playerAnim.SetFloat("moveSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCollider.position, .1f, groundLayer); //checks if collider overlap with any ground layer object
        //if jump button pressed and player in the ground
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                AudioManager.instance.PlaySFX(4);
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpSpeed);
            }

        }
        playerAnim.SetBool("isGrounded", isGrounded);
    }
    public void IncreaseAmmo() //called by a bullet when it is destroyed
    {
        if (ammo < maxAmmo)
        {
            ammo++;
        } 
        
        if (ammo > maxAmmo) 
        { 
            ammo = maxAmmo;
        }
    }


}

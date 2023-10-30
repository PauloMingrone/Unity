using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Horizontal movement variables
    float maxSpeed = 7f;
    float targetSpeed;
    float speedAcceleration;
    float accelerationModifier = 1.25f;
    float stopMovementFactor = 3.5f;
    Rigidbody2D playerRB;
    SpriteRenderer playerSR; //Sprite adjustments

    //Jump variables 
    [Header("Jump System")]
    float jumpSpeed = 8f;
    bool isGrounded = false;
    bool isJumping = false;
    float jumpCounter = 0f;
    float GROUND_DISTANCE_CHECK = 0.175f;
    Vector2 gravityVector;
    float jumpTimeLimit = 0.3f;
    float jumpModifier = 1.25f;
    float fallModifier = 2f;
    float fallMaxSpeed = -15f;
    public LayerMask groundLayer; // ground layer here
    public Transform groundCollider; // collider child object here

    //Shoot logic
    [Header("Shoot System")]
    public GameObject firePrefab;
    Transform playerTransform;
    Vector3 firePos;
    Quaternion fireRotation;
    int maxAmmo = 3;
    int ammo;

    //Keys and PowerUps
    bool key1;

    //for sprite animations
    Animator playerAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerTransform = GetComponent<Transform>();
        playerAnim = GetComponent<Animator>();

        gravityVector = new Vector2(0, -Physics2D.gravity.y);

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
        if (!playerAnim.GetBool("isDead"))
        {
            GameMaster.instance.increaseDeath();
            //Animation event will call RespawnPlayer
            playerAnim.SetBool("isDead", true);
            AudioManager.instance.PlaySFX(2);
        }
    }

    void RespawnPlayer()
    {
        //called by animator
        playerAnim.SetBool("isDead", false);
        transform.position = GameMaster.instance.ReturnPlayerPosition();
        Camera.main.transform.position = GameMaster.instance.ReturnCameraPosition();
        GameMaster.instance.MapRespawn();
    }

    void Move()
    {
        //Player RB veolicity becomes horizontal speed * 1 or -1 is Horizontal input Happens (Input manager)
        targetSpeed = Input.GetAxisRaw("Horizontal") * maxSpeed;
        if (Mathf.Abs(targetSpeed) > 0f)
        {
            speedAcceleration = (targetSpeed - playerRB.velocity.x) / accelerationModifier;
        }
        else
        {
            speedAcceleration = - playerRB.velocity.x * 1.5f;

        }
        playerRB.AddForce(speedAcceleration * Vector2.right);
        
        //logic to speedup slowdown when no horizontal input
        if ((targetSpeed == 0) && (Mathf.Abs(playerRB.velocity.x) < stopMovementFactor)) 
        {
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }


        if (playerRB.velocity.x < -0.1f && !playerSR.flipX)
        {
            playerSR.flipX = true;
        }
        else if (playerRB.velocity.x > 0.1f && playerSR.flipX)
        {
            playerSR.flipX = false;
        }
        playerAnim.SetFloat("moveSpeed", Mathf.Abs(playerRB.velocity.x));
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCollider.position, GROUND_DISTANCE_CHECK, groundLayer); //checks if collider overlap with any ground layer object
        //if jump button pressed and player in the ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpCounter = 0;
            AudioManager.instance.PlaySFX(4);
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpSpeed);

        }
        
        if(playerRB.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            playerRB.velocity += gravityVector * jumpModifier * Time.deltaTime;
            if (jumpCounter > jumpTimeLimit) 
            {
                isJumping = false;
            }
            
        }
        if(playerRB.velocity.y < 0)
        {
            isJumping = false;
            playerRB.velocity -= gravityVector * fallModifier * Time.deltaTime;
            if (playerRB.velocity.y < fallMaxSpeed)
            {
                playerRB.velocity = new Vector2 (playerRB.velocity.x, fallMaxSpeed);
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
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

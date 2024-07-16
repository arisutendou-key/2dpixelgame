using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float horizontalInput;
    private float GroundCheckRadius = 0.2f;
    private int jumpsLeft = 0;
    private Animator anim;

    public float moveSpeed = 8f;
    public float jumpSpeed = 7f;
    public Transform GroundCheckPoint;
    public LayerMask GroundLayer;
    public int maxJumps = 2;
    public GameObject losescreen;
    public int points = 0;
    public bool hasPowerup = false;
    public bool jumppowerup = false;
    public bool runpowerup = false;
   // public TextMeshProUGUI scoretext;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        losescreen.SetActive(false);
        
    }
    bool GroundCheck()
    {
        return Physics2D.OverlapCircle(GroundCheckPoint.position, GroundCheckRadius, GroundLayer);
    }
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(7f);
        hasPowerup = false;
        jumppowerup = false;
        runpowerup = false;

    }

    

    // Update is called once per frame
    void Update()
    {
        if (hasPowerup == false)
        {
            moveSpeed = 8f;
            jumpSpeed = 7f;

            horizontalInput = Input.GetAxisRaw("Horizontal");

            //moving left and right
            float nextVelocityX = horizontalInput * moveSpeed;

            if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            else if(horizontalInput > 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }

            //jumping
            bool grounded = GroundCheck();

            float nextVelocityY = rb2d.velocity.y;

            if (grounded && nextVelocityY <=0)
            {
                jumpsLeft = maxJumps;
            }

            if(Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                nextVelocityY = jumpSpeed;
                jumpsLeft -= 1;
            }
            //anim.SetFloat("XSpeed", Mathf.Abs(nextVelocityX));
            //anim.SetFloat("YSpeed", nextVelocityY);
            //anim.SetBool("Grounded", grounded);

            rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);

        }
        else if(hasPowerup && runpowerup)
        {
            moveSpeed = 12f;
            jumpSpeed = 7f;

            horizontalInput = Input.GetAxisRaw("Horizontal");

            //moving left and right
            float nextVelocityX = horizontalInput * moveSpeed;

            if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            else if(horizontalInput > 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }

            //jumping
            bool grounded = GroundCheck();

            float nextVelocityY = rb2d.velocity.y;

            if (grounded && nextVelocityY <=0)
            {
                jumpsLeft = maxJumps;
            }

            if(Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                nextVelocityY = jumpSpeed;
                jumpsLeft -= 1;
            }
            //anim.SetFloat("XSpeed", Mathf.Abs(nextVelocityX));
            //anim.SetFloat("YSpeed", nextVelocityY);
            //anim.SetBool("Grounded", grounded);

            rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);

        }
        else if(hasPowerup && jumppowerup)
        {
            moveSpeed = 8f;
            jumpSpeed = 12f;

            horizontalInput = Input.GetAxisRaw("Horizontal");

            //moving left and right
            float nextVelocityX = horizontalInput * moveSpeed;

            if(horizontalInput < 0)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            else if(horizontalInput > 0)
            {
                transform.localScale = new Vector3(1,1,1);
            }

            //jumping
            bool grounded = GroundCheck();

            float nextVelocityY = rb2d.velocity.y;

            if (grounded && nextVelocityY <=0)
            {
                jumpsLeft = maxJumps;
            }

            if(Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0)
            {
                nextVelocityY = jumpSpeed;
                jumpsLeft -= 1;
            }

            rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);

            //anim.SetFloat("XSpeed", Mathf.Abs(nextVelocityX));
            //anim.SetFloat("YSpeed", nextVelocityY);
            //anim.SetBool("Grounded", grounded);
        }

        
        

        //dying
        if(transform.position.y < -4)
        {
            Destroy(gameObject);
            losescreen.SetActive(true);
            
        }

        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //collecting stars
        if(other.tag == "star")
        {
            Destroy(other.gameObject);
            points += 1;
            //scoretext.text = "Score: " + points;

        }   
        //powerups
        if(other.tag == "jump")
        {
            hasPowerup = true;
            jumppowerup = true;
            StartCoroutine(PowerupCooldown());
            Destroy(other.gameObject);
        }
        if(other.tag == "run")
        {
            hasPowerup = true;
            runpowerup = true;
            StartCoroutine(PowerupCooldown());
            Destroy(other.gameObject);
        }
    } 

    

    // method to kill enemies by jumping on them
    void OnCollisionEnter2D(Collision2D other){
        // if colliding with the enemy
        if(other.gameObject.CompareTag("Enemy")){
            // if the player is falling down
            if(rb2d.velocity.y < 0){
                // if the player is above the enemy
                if(transform.position.y - other.gameObject.transform.position.y > 0.75){
                    // when the player jumps on the enemy, give the player
                    // a little boost up and destroy the enemy
                    rb2d.velocity = new Vector2(transform.position.x, 10);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}

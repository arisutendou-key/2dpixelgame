using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float horizontalInput;
    private float GroundCheckRadius = 0.2f;
    private int jumpsLeft = 0;
    private Animator anim;
    private cold c;

    public float moveSpeed = 8f;
    public float jumpSpeed = 7f;
    public Transform GroundCheckPoint;
    public LayerMask GroundLayer;
    public int maxJumps = 2;
    public GameObject losescreen;
    public bool hasPowerup = false;
    public bool jumppowerup = false;
    public bool runpowerup = false;
    public bool haswarmpowerup = false;
    public Vector3 respawnPoint;
    public bool died = false;
    public static PlayerMovement instance;
    public AudioSource enemyDeath;
    public AudioSource collectStar;
    public float starsCollected = 0;
    public bool oncooldownzone = false;

   // public TextMeshProUGUI scoretext;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        c = FindObjectOfType<cold>();
        losescreen.SetActive(false);
        respawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        instance = this;
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
        haswarmpowerup = false;
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
        else if(hasPowerup && haswarmpowerup)
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
        

        //dying
        if(transform.position.y < -4)
        {
            //Destroy(gameObject);
            died = true;
            losescreen.SetActive(true);
        }

        // if the player dies, they'll go back to their last checkpoint
        if(died){
            transform.position = respawnPoint;
            died = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //collecting stars
        if(other.tag == "star")
        {
            Destroy(other.gameObject);
            starsCollected += 1;
            //scoretext.text = "Score: " + starsCollected;
            collectStar.Play();
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
        if(other.tag == "warm")
        {
            hasPowerup = true;
            haswarmpowerup = true;
            StartCoroutine(PowerupCooldown());
            Destroy(other.gameObject);
        }
        if(other.tag == "cooldownzone")
        {
            oncooldownzone = true;
            c.coolingnotice.SetActive(true);
        }
        if(other.tag == "normalground")
        {
            oncooldownzone = false;
            c.coolingnotice.SetActive(false);
        }

        //portals
        if(other.tag == "neptune")
        {
            SceneManager.LoadScene("Neptune");

        }
        if(other.tag == "venus")
        {
            SceneManager.LoadScene("Venus");

        }

        // setting respawn point when the player touches a checkpoint
        if(other.gameObject.CompareTag("Checkpoint")){
            respawnPoint = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
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

                    // plays killing sound effect
                    enemyDeath.Play();
                }
            }
        }
    }
}

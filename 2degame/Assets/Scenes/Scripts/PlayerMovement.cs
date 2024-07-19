using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

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
    public bool hasPowerup = false;
    public bool jumppowerup = false;
    public bool runpowerup = false;
    public bool haswarmpowerup = false;
    public Vector3 respawnPoint;
    public bool died = false;
    public static PlayerMovement instance;
    public AudioSource enemyDeath;
    public AudioSource collectStar;
    public static float starsCollected = 0;
    public GameObject freezenotice;
    public GameObject warmupnotice;
    public bool freezing = false;
    public AudioSource jumpPowerUpSFX;
    public AudioSource useJumpSFX;
    public AudioSource runPowerUpSFX;
    public AudioSource useRunSFX;
    public AudioClip[] possibleStarSFX;
    public int maxHealth = 100;
    public int currentHealth;
    public int damageAmount = 15;
    public AudioSource losingHpSFX;
    private float nextVelocityX = 0;

   // public TextMeshProUGUI scoretext;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        respawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        instance = this;

        freezenotice.SetActive(false);
        warmupnotice.SetActive(false);
        freezing = false;

        currentHealth = maxHealth;
        HealthBar.instance.SetMaxHealth(100);

        starsCollected = 0;
        //GameManager.totalStarsAmount = 0;
    }
    bool GroundCheck()
    {
        return Physics2D.OverlapCircle(GroundCheckPoint.position, GroundCheckRadius, GroundLayer);
    }

    IEnumerator coldtimer()
    {
        yield return new WaitForSeconds(10f);

        if(haswarmpowerup == false)
        {
            freezing = true;
        }
    }

    IEnumerator freezetimer()
    {
        yield return new WaitForSeconds(5f);
        if(haswarmpowerup == false)
        {
            died = true;
        }
        else
        {
            freezing = false;
        }  
    }
    IEnumerator RunPowerupCooldown(GameObject other)
    {
        yield return new WaitForSeconds(7f);
        //hasPowerup = false;
        runpowerup = false;
        other.gameObject.SetActive(true);
        //haswarmpowerup = false;
    }

    IEnumerator JumpPowerupCooldown(GameObject other)
    {
        yield return new WaitForSeconds(7f);
        //hasPowerup = false;
        jumppowerup = false;
        other.gameObject.SetActive(true);
        //haswarmpowerup = false;
    }

    IEnumerator warmPowerupCooldown()
    {
        yield return new WaitForSeconds(7f);
        hasPowerup = false;
        haswarmpowerup = false;
        warmupnotice.SetActive(false);
 
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPowerup == false)
        {
            moveSpeed = 8f;
            jumpSpeed = 7f;
        } else {
            if(runpowerup){
                moveSpeed = 12f;
                //jumpSpeed = 7f;   

            } else if(jumppowerup){
                //moveSpeed = 8f;
                jumpSpeed = 12f;

            } else if(haswarmpowerup){
                moveSpeed = 8f;
                jumpSpeed = 7f;
            }
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Math.Abs(horizontalInput) == 1 && nextVelocityX == 0){
            if(runpowerup && !useRunSFX.isPlaying){
                useRunSFX.Play();
            }
        }

        //moving left and right
        nextVelocityX = horizontalInput * moveSpeed;

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

            if(jumppowerup && !useJumpSFX.isPlaying){
                useJumpSFX.Play();
            }
        }
        anim.SetFloat("XSpeed", Mathf.Abs(nextVelocityX));
        anim.SetFloat("YSpeed", nextVelocityY);
        anim.SetBool("Grounded", grounded);

        rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);

        //dying
        if(transform.position.y < -4)
        {
            //Destroy(gameObject);
            died = true;
        }

        // if the player dies, they'll go back to their last checkpoint
        if(died){
            transform.position = respawnPoint;
            currentHealth -= damageAmount;
            HealthBar.instance.SetHealth(currentHealth);
            losingHpSFX.Play();
            // setting camera to player's position when respawning
            CameraScript.instance.transform.position = new Vector3(transform.position.x, transform.position.y, CameraScript.instance.transform.position.z);
            died = false;
        }

        if(currentHealth <= 0){
            SceneManager.LoadScene("GameOver");
        }

        //print(HealthBar.instance.slider.value);

        //warming/cooling for venus and neptune
        if (SceneManager.GetActiveScene().name == "Neptune" || SceneManager.GetActiveScene().name == "Venus"){
            if(haswarmpowerup)
            {
                StopCoroutine("coldtimer");
                StopCoroutine("freezetimer");
                warmupnotice.SetActive(true);
                freezenotice.SetActive(false);
                freezing = false;

            }
            else if(freezing == false)
            {
                StartCoroutine("coldtimer");
            }
            else if(freezing)
            {
                freezenotice.SetActive(true);
                StartCoroutine("freezetimer");
            }
        }

        if(!jumppowerup && !runpowerup && !haswarmpowerup){
            hasPowerup = false;
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
            collectStar.clip = possibleStarSFX[UnityEngine.Random.Range(0,4)];
            
            collectStar.Play();
        }   
        //powerups
        if(other.tag == "jump")
        {
            hasPowerup = true;
            jumppowerup = true;
            jumpPowerUpSFX.Play();
            other.gameObject.SetActive(false);
            StartCoroutine(JumpPowerupCooldown(other.gameObject));
        }
        if(other.tag == "run")
        {
            hasPowerup = true;
            runpowerup = true;
            runPowerUpSFX.Play();
            other.gameObject.SetActive(false);
            StartCoroutine(RunPowerupCooldown(other.gameObject));
            
        }
        if(other.tag == "warm")
        {
            hasPowerup = true;
            haswarmpowerup = true;
            freezing = false;
            StartCoroutine(warmPowerupCooldown());
            Destroy(other.gameObject);
        }
        if(other.tag == "square")
        {
            transform.position = new Vector3(104,-1,0);
        }
        if(other.tag == "square2")
        {
            transform.position = new Vector3(-11,0,0);
        }
        if(other.tag == "end")
        {
            SceneManager.LoadScene("Endscreen");
        }

        //portals
        if(other.tag == "Neptune")
        {
            //UIManager.instance.levelComplete = true;
            //SceneManager.LoadScene("Neptune");
            UIManager.lastScene = "Earth";
            UIManager.nextScene = "Neptune";
            SceneManager.LoadScene("LevelComplete");
        }
        
        if(other.tag == "venus")
        {
            //UIManager.instance.levelComplete = true;
            //SceneManager.LoadScene("Venus");
            UIManager.lastScene = "Neptune";
            UIManager.nextScene = "Venus";
            SceneManager.LoadScene("LevelComplete");
        }
        
        

        // setting respawn point when the player touches a checkpoint
        if(other.gameObject.CompareTag("Checkpoint")){
            respawnPoint = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            other.gameObject.GetComponent<Animator>().SetBool("Activated", true);
            //other.gameObject.GetComponent<Animator>().SetBool("Activated", false);
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
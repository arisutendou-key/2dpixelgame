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

    public float moveSpeed = 8f;
    public float jumpSpeed = 6f;
    public Transform GroundCheckPoint;
    public LayerMask GroundLayer;
    public int maxJumps = 2;
    public GameObject losescreen;
    public int points = 0;
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

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //moving left and right
        float nextVelocityX = horizontalInput * moveSpeed;

        if(horizontalInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),Mathf.Abs(transform.localScale.x),Mathf.Abs(transform.localScale.x));
        }
        else if(horizontalInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),Mathf.Abs(transform.localScale.x),Mathf.Abs(transform.localScale.x));
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

        //dying
        if(transform.position.y < -4)
        {
            Destroy(gameObject);
            losescreen.SetActive(true);
            
        }

        
    }
    //collecting stars
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "star")
        {
            Destroy(other.gameObject);
            points += 1;
            //scoretext.text = "Score: " + points;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float horizontalInput;

    public float moveSpeed = 10f;
    public float jumpSpeed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // **** FOR LOW GRAVITY JUMPING FEEL:
        // lower the player's gravity scale when in a specific scene
        horizontalInput = Input.GetAxisRaw("Horizontal");

        float nextVelocityX = horizontalInput * moveSpeed;

        if(horizontalInput < 0){ // facing left
            // Vector3 just sets all the corresponding dimensions
            transform.localScale = new Vector3(-1, 1, 1);
        } else if(horizontalInput > 0){ // facing right
            transform.localScale = new Vector3(1, 1, 1);
        }

        float nextVelocityY = rb2d.velocity.y;

        if(Input.GetKeyDown(KeyCode.Space)){
            nextVelocityY = jumpSpeed;
        }

        // setting all the velocity to the rigidbody
        rb2d.velocity = new Vector2(nextVelocityX, nextVelocityY);
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

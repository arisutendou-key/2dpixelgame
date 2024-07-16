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
}

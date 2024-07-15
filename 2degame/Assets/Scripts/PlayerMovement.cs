using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float horizontalInput;
    private float GroundCheckRadius = 0.2f;
    private int jumpsLeft = 0;

    public float moveSpeed = 10f;
    public float jumpSpeed = 7.5f;
    public Transform GroundCheckPoint;
    public LayerMask GroundLayer;
    public int maxJumps = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
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

        
    }
}

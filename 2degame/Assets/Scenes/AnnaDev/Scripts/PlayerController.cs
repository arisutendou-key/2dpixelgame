using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float horizontalInput;

    public float moveSpeed = 10f;
    public float jumpSpeed = 12f;
    public GameObject[] totalStars;
    public float starsCollected = 0;
    public float starPercentage;
    public GameObject stars;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, 0);
        totalStars = GameObject.FindGameObjectsWithTag("star");
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

        print("All stars: " + totalStars.Length);
        print("Stars collected: " + starsCollected);
        starPercentage = (starsCollected / totalStars.Length) * 100;

        print("Star %: " + starPercentage);

        if(starPercentage <= 34){
            print("1 star!!!!!");
        } else if(starPercentage <= 67){
            print("2 stars!!!!!");
        } else if(starPercentage <= 100){
            print("3 stars yayyyy!!!!!");
        }

        for(int i = 0; i < 10; i++){
            Instantiate(stars, Vector2(Ran))
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("star")){
            starsCollected++;
            Destroy(other.gameObject);
        }
    }
}

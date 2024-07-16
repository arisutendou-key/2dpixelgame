using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    //public GameObject player;
    public float moveSpeed = 4f;
    private Vector2 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //player = FindObjectOfType<PlayerMovement>().gameObject;

        // sets the direction of the projectile once and leaves it as that
        lookDirection = (PlayerMovement.instance.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // making the projectile move in the direction of the player at
        // a specified speed
        transform.Translate(moveSpeed * Time.deltaTime * lookDirection);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // should destroy projectile when it touches anything but the enemy or itself
        if(!other.gameObject.CompareTag("Enemy") && other.gameObject != gameObject){
            // destroying both the projectile and the player if the two hit
            Destroy(gameObject);
            
            if(other.gameObject == PlayerMovement.instance.gameObject){
                // change this to just teleport the player to a checkpoint later
                //Destroy(other.gameObject);
                PlayerMovement.instance.died = true;
            }
        }
    }
}

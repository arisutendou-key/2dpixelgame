using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProjectileScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    //public GameObject player;
    public float moveSpeed = 4f;
    private Vector2 lookDirection;
    public Sprite earthShootSprite;
    public Sprite neptuneShootSprite;
    public Sprite venusShootSprite;

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

        if(SceneManager.GetActiveScene().name == "Earth"){
            gameObject.GetComponent<SpriteRenderer>().sprite = earthShootSprite;
        } else if(SceneManager.GetActiveScene().name == "Neptune"){
            gameObject.GetComponent<SpriteRenderer>().sprite = neptuneShootSprite;
        } else if(SceneManager.GetActiveScene().name == "Venus"){
            gameObject.GetComponent<SpriteRenderer>().sprite = venusShootSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // should destroy projectile when it touches anything but the enemy or itself
        if(!other.gameObject.CompareTag("Enemy") && other.gameObject != gameObject && !other.gameObject.CompareTag("Background")){
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

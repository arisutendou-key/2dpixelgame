using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public float detectionDistance = 8f;
    public GameObject enemyProjectPrefab;
    //public bool detectedPlayer = false;
    public int timer = 0;
    public int repeatEnd = 500;
    public Sprite earthShootSprite;
    public Sprite neptuneShootSprite;
    public Sprite venusShootSprite;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // when the distance between the player and the enemy is within the specified
        // distance, the enemy will immediately spawn a projectile towards the player.
        // then, after the specified repetition time, it will spawn a projectile again
        if(Math.Abs((player.transform.position - transform.position).magnitude) <= detectionDistance){
            print("in range");
            if(timer == 0){
                ShootProjectile();
            }

            timer++;

            if(timer >= repeatEnd){
                timer = 0;
            }
        } else {
            timer = repeatEnd;
        }

        //print(Math.Abs((player.transform.position - transform.position).magnitude));
    }

    private void ShootProjectile(){
        if(SceneManager.GetActiveScene().name == "Earth"){
            enemyProjectPrefab.GetComponent<SpriteRenderer>().sprite = earthShootSprite;
        } else if(SceneManager.GetActiveScene().name == "Neptune"){
            enemyProjectPrefab.GetComponent<SpriteRenderer>().sprite = neptuneShootSprite;
        } else if(SceneManager.GetActiveScene().name == "Venus"){
            enemyProjectPrefab.GetComponent<SpriteRenderer>().sprite = venusShootSprite;
        }

        Instantiate(enemyProjectPrefab, transform.position, enemyProjectPrefab.transform.rotation);
    }
}

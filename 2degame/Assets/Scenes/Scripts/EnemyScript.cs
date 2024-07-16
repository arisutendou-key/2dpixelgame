using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public float detectionDistance = 8f;
    public GameObject enemyProjectPrefab;
    //public bool detectedPlayer = false;
    public int timer = 0;
    public int repeatEnd = 500;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // when the distance between the player and the enemy is within the specified
        // distance, the enemy will immediately spawn a projectile towards the player.
        // then, after the specified repetition time, it will spawn a projectile again
        if(Math.Abs((player.transform.position - transform.position).magnitude) <= detectionDistance){
            if(timer == 0){
                ShootProjectile1();
            }

            timer++;

            if(timer >= repeatEnd){
                timer = 0;
            }
        } else {
            timer = repeatEnd;
        }
    }

    private void ShootProjectile1(){
        Instantiate(enemyProjectPrefab, transform.position, enemyProjectPrefab.transform.rotation);
    }
}

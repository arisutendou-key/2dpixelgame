using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public static CameraScript instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Earth"){
            if(player.transform.position.y < 0 || player.transform.position.y > 9.25){
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            } else {
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
        } else if(SceneManager.GetActiveScene().name == "Neptune"){
            if(player.transform.position.y < 0 || player.transform.position.y > 20){
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            } else {
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
        } else if(SceneManager.GetActiveScene().name == "Venus"){
            if(player.transform.position.y < 0 || player.transform.position.y > 9.75){
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            } else {
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
            }
        }
        
        /*
        // (add upper bound later)
        if(player.transform.position.y < 0 || player.transform.position.y > 9.25){
            if(SceneManager.GetActiveScene().name == "Earth"){
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }
        } else if(player.transform.position.y < 0 || player.transform.position.y > 20){
            if(SceneManager.GetActiveScene().name == "Neptune"){
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }
        } else if(player.transform.position.y < 0 || player.transform.position.y > 9.25){
            if(SceneManager.GetActiveScene().name == "Venus"){
                // follows the player's x, but keeps the camera's y and z as it moves 
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }
        } else {
            // follows the player's x, but keeps the camera's y and z as it moves 
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        */
    }
}

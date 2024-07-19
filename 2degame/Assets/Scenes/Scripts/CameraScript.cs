using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public static CameraScript instance;

    void Start()
    {
        instance = this;
    }

    void LateUpdate()
    {
        // (add upper bound later)
        if(player.transform.position.y < 0 || player.transform.position.y > 9.25){
            // follows the player's x, but keeps the camera's y and z as it moves 
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        } else {
            // follows the player's x, but keeps the camera's y and z as it moves 
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}

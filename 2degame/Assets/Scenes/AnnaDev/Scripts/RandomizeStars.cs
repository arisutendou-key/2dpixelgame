using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeStars : MonoBehaviour
{
    public GameObject stars;
    // Start is called before the first frame update
    void Start()
    {
        // creates a new star on the screen 8 times in a random position
        for(int i = 0; i < 8; i++){
            Instantiate(stars, new Vector2(Random.Range(-750, 750), Random.Range(-400, 400)), transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

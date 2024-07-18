using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip mainBGMusic;
    public AudioClip earthBGMusic;
    public AudioClip coldBGMusic;
    public AudioClip hotBGMusic;
    public AudioClip spaceBGMusic;
    public AudioSource BGMusic;
    public GameObject[] totalStars;
    public float starPercentage;
    public GameObject stars;
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; 

        if(SceneManager.GetActiveScene().name == "StartScreen"){
            BGMusic.clip = mainBGMusic;

            // creates a new star on the screen 8 times in a random position
            for(int i = 0; i < 20; i++){
                stars.transform.localScale = new Vector2(0.5f,0.5f);

                Instantiate(stars, new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), transform.rotation);
            }
        } else if(SceneManager.GetActiveScene().name == "Earth"){
            BGMusic.clip = earthBGMusic;
        } else if(SceneManager.GetActiveScene().name == "Neptune"){
            BGMusic.clip = coldBGMusic;
        } else if(SceneManager.GetActiveScene().name == "Venus"){
            BGMusic.clip = hotBGMusic;
        } else if(SceneManager.GetActiveScene().name == "Space"){
            BGMusic.clip = spaceBGMusic;
        }

        BGMusic.Play();

        // figures out how many stars there are in the level
        totalStars = GameObject.FindGameObjectsWithTag("star");
    }

    // Update is called once per frame
    void Update()
    {
        // creates a percentage of the stars that the player has collected
        // out of all the stars in the level
        starPercentage = (PlayerMovement.instance.starsCollected / totalStars.Length) * 100;
    }
    
    public void Venus()
    {
        SceneManager.LoadScene("Venus");
    }
    public void Neptune()
    {
        SceneManager.LoadScene("Neptune");
    }
}

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
    public bool muted = false;
    public GameObject[] totalStars;
    public float starPercentage;

    // Start is called before the first frame update
    void Start()
    {
        // *** change to refer to scene name instead of build index later
        if(SceneManager.GetActiveScene().name == "Main Menu"){
            BGMusic.clip = mainBGMusic;
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
        /*
        if(Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        */

        print("All stars: " + totalStars.Length);
        print("Stars collected: " + PlayerMovement.instance.starsCollected);

        // creates a percentage of the stars that the player has collected
        // out of all the stars in the level
        starPercentage = (PlayerMovement.instance.starsCollected / totalStars.Length) * 100;

        print("Star %: " + starPercentage);

        // if the percentage of stars that the player has collected is
        // <= ~50%, the level gets a 1 star rating
        // if it's < 100%, it's a 2 star rating
        // a full 100% gets a 3 star rating
        if(starPercentage <= 50){
            print("1 star!!!!!");
        } else if(starPercentage < 100){
            print("2 stars!!!!!");
        } else if(starPercentage == 100){
            print("3 stars yayyyy!!!!!");
        }
    }

    public void MuteMusic(){
        if(muted){
            BGMusic.Play();
        } else {
            BGMusic.Pause();
        }

        muted = !muted;
    }
}

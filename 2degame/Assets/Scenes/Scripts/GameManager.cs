using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioClip mainBGMusic;
    public AudioClip earthBGMusic;
    public AudioClip coldBGMusic;
    public AudioClip hotBGMusic;
    public AudioClip spaceBGMusic;
    public AudioSource BGMusic;
    public GameObject[] totalStars;
    public static int totalStarsAmount;
    public static float gameTotalStarsCollected;
    public static int gameTotalStars;
    public static float starPercentage;
    public GameObject starPrefab;
    public static GameManager instance;
    public bool muted = false;
    public GameObject jumpIcon;
    public GameObject runIcon;
    public GameObject burnOverlay;
    public GameObject freezeOverlay;
    public float overlayOpacity = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this; 

        /*
        if(SceneManager.GetActiveScene().name == "StartScreen" || SceneManager.GetActiveScene().name == "Controls" || 
        SceneManager.GetActiveScene().name == "Endscreen" || SceneManager.GetActiveScene().name == "GameOver" || 
        SceneManager.GetActiveScene().name == "LevelComplete"){
            jumpIcon = null;
            runIcon = null;
            burnOverlay = null;
            freezeOverlay = null;
        }
        */

        // figures out how many stars there are in the level
        totalStars = GameObject.FindGameObjectsWithTag("star");

        if(SceneManager.GetActiveScene().name == "StartScreen"){
            BGMusic.clip = mainBGMusic;

            // creates a new star on the screen 8 times in a random position
            for(int i = 0; i < 20; i++){
                starPrefab.transform.localScale = new Vector2(0.5f,0.5f);

                Instantiate(starPrefab, new Vector2(Random.Range(-8f, 8f), Random.Range(-3.3f, 4f)), transform.rotation);
            }
        } else if(SceneManager.GetActiveScene().name == "Earth"){
            BGMusic.clip = earthBGMusic;
            totalStarsAmount = totalStars.Length;
        } else if(SceneManager.GetActiveScene().name == "Neptune"){
            BGMusic.clip = coldBGMusic;
            totalStarsAmount = totalStars.Length;
        } else if(SceneManager.GetActiveScene().name == "Venus"){
            BGMusic.clip = hotBGMusic;
            totalStarsAmount = totalStars.Length;
        } else if(SceneManager.GetActiveScene().name == "Endscreen"){
            BGMusic.clip = spaceBGMusic;
        } else {
            BGMusic.clip = null;
        }

        BGMusic.Play();

        jumpIcon.SetActive(false);
        runIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // creates a percentage of the stars that the player has collected
        // out of all the stars in the level
        starPercentage = (PlayerMovement.starsCollected / totalStarsAmount) * 100;

        if(SceneManager.GetActiveScene().name == "Neptune"){
            overlayOpacity += 0.0003f;
        } else if(SceneManager.GetActiveScene().name == "Venus"){
            overlayOpacity += 0.00018f;
        }

        if(!freezeOverlay.activeSelf && !burnOverlay.activeSelf){
            overlayOpacity = 0;
        }

        if(freezeOverlay.activeSelf){
            freezeOverlay.GetComponent<Image>().color = new Vector4(0, 222, 255, overlayOpacity);
        } else {
            //overlayOpacity = 0;
            freezeOverlay.GetComponent<Image>().color = new Vector4(0, 222, 255, 0);
        }
        
        if(burnOverlay.activeSelf){
            burnOverlay.GetComponent<Image>().color = new Vector4(255, 0, 0, overlayOpacity);
        } else {
            //overlayOpacity = 0;
            burnOverlay.GetComponent<Image>().color = new Vector4(0, 222, 255, 0);
        }
    }

    public void Venus()
    {
        SceneManager.LoadScene("Venus");
    }
    public void Neptune()
    {
        SceneManager.LoadScene("Neptune");
    }

    public void MuteMusic(){
        if(muted){
            GameManager.instance.BGMusic.Play();
        } else {
            GameManager.instance.BGMusic.Pause();
        }

        muted = !muted;
    }
}
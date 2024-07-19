using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public bool levelComplete = false;
    public TextMeshProUGUI levelRatingText;
    public TextMeshProUGUI collectedStarsText;
    //public GameObject levelCompleteScreen;
    public static string lastScene;
    public static string nextScene;
    public bool onControls;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //levelCompleteScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if the percentage of stars that the player has collected is
        // <= ~50%, the level gets a 1 star rating
        // if it's < 100%, it's a 2 star rating
        // a full 100% gets a 3 star rating
        if(GameManager.starPercentage <= 50){
            levelRatingText.text = "Level Rating: 1 star";
        } else if(GameManager.starPercentage < 100){
            levelRatingText.text = "Level Rating: 2 stars";
        } else if(GameManager.starPercentage >= 100){
            levelRatingText.text = "Level Rating: 3 stars";
        }

        collectedStarsText.text = "Stars Collected: " + PlayerMovement.starsCollected + " / " + GameManager.totalStarsAmount;

        /*
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(SceneManager.GetActiveScene().name == "StartScreen" || SceneManager.GetActiveScene().name == "Earth" || 
            SceneManager.GetActiveScene().name == "Neptune" || SceneManager.GetActiveScene().name == "Venus"){
                if(!onControls){
                    lastScene = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene("Controls");
                } else {
                    SceneManager.LoadScene(lastScene);
                }

                onControls = !onControls;
            }
        }
        */
    }

    public void earth()
    {
        SceneManager.LoadScene("Earth");
    }
    public void controls()
    {
        //lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Controls");
    }
    public void mainscreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void ContinueLevel(){
        //levelCompleteScreen.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameManager.gameTotalStarsCollected += PlayerMovement.starsCollected;
        GameManager.gameTotalStars += GameManager.totalStarsAmount;
        PlayerMovement.starsCollected = 0;
        GameManager.totalStarsAmount = 0;
        
        SceneManager.LoadScene(nextScene);
    }

    public void PreviousLevel(){
        SceneManager.LoadScene(lastScene);
    }

    /*
    public void backButton(){
        //SceneManager.LoadScene(lastScene);
        SceneManager.LoadScene("StartScreen");
    }
    */
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public bool levelComplete = false;
    public TextMeshPro levelRatingText;
    public TextMeshPro collectedStarsText;
    public GameObject levelCompleteScreen;
    //public string lastScene;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        levelCompleteScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if the percentage of stars that the player has collected is
        // <= ~50%, the level gets a 1 star rating
        // if it's < 100%, it's a 2 star rating
        // a full 100% gets a 3 star rating
        if(GameManager.instance.starPercentage <= 50){
            levelRatingText.text = "Level Rating: 1 star";
        } else if(GameManager.instance.starPercentage < 100){
            levelRatingText.text = "Level Rating: 2 star";
        } else if(GameManager.instance.starPercentage == 100){
            levelRatingText.text = "Level Rating: 3 star";
        }

        collectedStarsText.text = "Stars Collected: " + PlayerMovement.instance.starsCollected + " / " + GameManager.instance.totalStars.Length;
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
        levelCompleteScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
    public void backButton(){
        //SceneManager.LoadScene(lastScene);
        SceneManager.LoadScene("StartScreen");
    }
    */
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource mainBGMusic;
    public AudioSource earthBGMusic;
    public AudioSource coldBGMusic;
    public AudioSource hotBGMusic;
    public AudioSource spaceBGMusic;

    // Start is called before the first frame update
    void Start()
    {
        // *** change to refer to scene name instead of build index later
        if(SceneManager.GetActiveScene().name == "Main Menu"){
            mainBGMusic.Play();
        } else if(SceneManager.GetActiveScene().name == "Earth"){
            earthBGMusic.Play();
        } else if(SceneManager.GetActiveScene().name == "Neptune"){
            coldBGMusic.Play();
        } else if(SceneManager.GetActiveScene().name == "Venus"){
            hotBGMusic.Play();
        } else if(SceneManager.GetActiveScene().name == "Space"){
            spaceBGMusic.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        */
    }
}

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

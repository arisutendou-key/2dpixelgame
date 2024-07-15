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
        if(SceneManager.GetActiveScene().buildIndex == 1){
            mainBGMusic.Play();
        } else if(SceneManager.GetActiveScene().buildIndex == 2){
            earthBGMusic.Play();
        } else if(SceneManager.GetActiveScene().buildIndex == 3){
            coldBGMusic.Play();
        } else if(SceneManager.GetActiveScene().buildIndex == 4){
            hotBGMusic.Play();
        } else if(SceneManager.GetActiveScene().buildIndex == 5){
            spaceBGMusic.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
